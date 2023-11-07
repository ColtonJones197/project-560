using Microsoft.AspNetCore.Mvc;
using netapi.Models;
using NuGet.Protocol;
using System.Text.Json;

namespace netapi.Controllers
{
    public class DataController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private HttpClient client;

        private const string connectionString = @"Server=DESKTOP-VT4KSCJ\SQLEXPRESS;Database=ChessLocal;Integrated Security=SSPI;Encrypt=False";

        [HttpPut("{title}")]
        public async Task<IReadOnlyList<Player>> AddTitledPlayers(string title)
        {
            // lookup all titled player usernames from chesscomapi
            // for each player username, look up the player and get all their info
            // add that info to the db (asynchronously) using SqlPlayerRepository
            // return all players added?
            List<Player> players = new();

            switch (title)
            {
                case "GM":
                case "FM":
                case "CM":
                case "IM":
                    break;
                default:
                    return players;
            }
            
            string requestUrl = $"https://api.chess.com/pub/titled/{title}";

            var usernames = await ProcessUsernamesAsync(client, requestUrl);

            return players;
        }

        public DataController(IConfiguration configuration)
        {
            _configuration = configuration;

            client = new();
            //client.Timeout = new TimeSpan(10000);
        }

        static async Task<List<string>> ProcessUsernamesAsync(HttpClient cli, string url)
        {
            await using Stream stream =
                await cli.GetStreamAsync(url);
            JsonElement playerList =
                await JsonSerializer.DeserializeAsync<JsonElement>(stream);

            string[]? playerArray = playerList.GetProperty("players").Deserialize<string[]>();

            List<string> addedPlayers = new();

            uint fails = 0;

            if (playerArray != null)
            {
                var playerClient = new HttpClient();
                playerClient.DefaultRequestHeaders.Add("User-Agent", "ChessLink");

                foreach (string username in playerArray)
                {
                    string playerUrl = $"https://api.chess.com/pub/player/{username}";

                    //var response = await playerClient.GetStringAsync(playerUrl);

                    //var responseJson = JsonSerializer.DeserializeAsync<JsonElement>(responseBody);
                    //Console.WriteLine(responseJson);
                    // Also fetch player info

                    try
                    {
                        await using Stream playerStream = await playerClient.GetStreamAsync(playerUrl);

                        var player = JsonSerializer.Deserialize<PlayerRecord>(playerStream);

                        Player created = await Task.Run(() => new SqlPlayerRepository(connectionString).CreatePlayer(
                            player.username,
                            player.player_id,
                            player.avatar,
                            player.title,
                            player.status,
                            player.name)
                        );

                        addedPlayers.Add(created.Username);
                    }
                    catch(Exception e)
                    {
                        fails++;
                    }
                    //string? avatar = playerJson.GetProperty("avatar").Deserialize<string>() ?? null;
                    //string? name = playerJson.GetProperty("name").Deserialize<string>() ?? null;
                    //Console.WriteLine($"{name}: {avatar}");

                }
            }

            Console.WriteLine($"# of occurred fails: {fails}");

            return addedPlayers;
        }

        //private static Player parsePlayerJson(JsonElement element)
        //{

        //}

        private record class PlayerRecord
        {
            public int player_id { get; set; }
            public string username { get; set; }
            public string? name { get; set; } = null;
            public string? country { get; set; } = null;
            public string? title { get; set; } = null;
            public string? status { get; set; } = null;

            public string? avatar { get; set; } = null;

        }
    }
}
