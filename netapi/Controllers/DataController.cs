using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using netapi.Models;
using NuGet.Packaging.Signing;
using NuGet.Protocol;
using System.Diagnostics;
using System.Media;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace netapi.Controllers
{
    public class DataController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private HttpClient client;

        private const string connectionString = @"Server=DESKTOP-VT4KSCJ\SQLEXPRESS;Database=ChessLocal;Integrated Security=SSPI;Encrypt=False";

        [HttpPut("/games/{username}")]
        public async Task<IReadOnlyList<Game>> AddPlayerGames(string username)
        {
            //List<Game> games = new();

            string archivesUrl = $"https://api.chess.com/pub/player/{username}/games/archives";

            var processedGames = await ProcessArchivesAsync(client, archivesUrl);

            return processedGames;
        }

        private async Task<List<Game>> ProcessArchivesAsync(HttpClient client, string archivesUrl)
        {
            var allGames = new List<Game>();

            await using Stream stream =
                await client.GetStreamAsync(archivesUrl);
            JsonElement archiveList =
                await JsonSerializer.DeserializeAsync<JsonElement>(stream);
            string[]? archiveArray = archiveList.GetProperty("archives").Deserialize<string[]>();

            if (archiveArray is null) return null;

            foreach(var gameArchiveUrl in archiveArray)
            {
                await using Stream gameStream =
                    await client.GetStreamAsync(gameArchiveUrl);
                JsonElement allGameJson =
                    await JsonSerializer.DeserializeAsync<JsonElement>(gameStream);
                var games = allGameJson.GetProperty("games").Deserialize<GameRecord[]>();
                var gameRepo = new SqlGameRepository(connectionString);
                foreach (GameRecord game in games)
                {
                    try
                    {
                        

                        //add white and black players to db if needed which maybe it is who knows lol
                        string whiteUser = game.white.GetProperty("username").Deserialize<string>();
                        string blackUser = game.black.GetProperty("username").Deserialize<string>();

                        await TryAddPlayer(whiteUser);
                        await TryAddPlayer(blackUser);

                        string opening = GetOpeningFromPgn(game.pgn);
                        int result = GetResultFromPgn(game.pgn);
                        DateTime endTime = DateTimeOffset.FromUnixTimeSeconds(game.end_time).DateTime;
                        //try
                        //{
                        Game added = gameRepo.CreateGame(
                        game.url,
                        whiteUser,
                        blackUser,
                        result,
                        game.pgn,
                        endTime,
                        game.time_class,
                        game.rules,
                        endTime,
                        null,
                        opening
                        );

                        allGames.Add(added);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Failed");
                    }
                    
                    //}
                    //catch(Exception e)
                    //{
                    //    Console.WriteLine("failed");
                    //}
                    
                    //gameRepo.CreateGame();
                }
            }

            return allGames;
        }

        public static int GetResultFromPgn(string pgn)
        {
            string pattern = "\\[Result \\\"(\\d+\\/?\\d*-\\d+\\/?\\d*)";
            var reg = new Regex(pattern);
            Match match = reg.Match(pgn);
            string resultStr = match.Groups[1].ToString();
            if (resultStr == "1/2-1/2") return 0;
            if (resultStr == "1-0") return 1;
            if (resultStr == "0-1") return -1;

            throw new ArgumentException();
        }

        public static string GetOpeningFromPgn(string pgn)
        {
            string pattern = "\\[ECO\\s+\\W+(\\w+)";
            var reg = new Regex(pattern);
            Match match = reg.Match(pgn);
            
            return match.Groups[1].ToString();
        }

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
            client.DefaultRequestHeaders.Add("User-Agent", "ChessLink");
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

        private async Task<Player?> TryAddPlayer(string username)
        {
            var playerUrl = $"https://api.chess.com/pub/player/{username}";
            await using Stream playerStream = await client.GetStreamAsync(playerUrl);

            var player = JsonSerializer.Deserialize<PlayerRecord>(playerStream);
            try
            {
                return await Task.Run(() => new SqlPlayerRepository(connectionString).CreatePlayer(
                player.username,
                player.player_id,
                player.avatar,
                player.title,
                player.status,
                player.name));
            }
            catch (Exception e)
            {
                return null;
            }
            
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

        private record class GameRecord
        {
            public string url { get; set; }

            public string pgn { get; set; }
            public string time_control { get; set; }
            public long end_time { get; set; }
            
            public string time_class { get; set; }
            public string rules { get; set; }
            public JsonElement white { get; set; }
            public JsonElement black { get; set; }
        }

    }
}