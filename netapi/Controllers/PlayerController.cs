using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netapi.Models;

namespace netapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private const string connectionString = @"Server=DESKTOP-VT4KSCJ\SQLEXPRESS;Database=ChessLocal;Integrated Security=SSPI;Encrypt=False";

        [HttpGet()]
        public IReadOnlyList<Player> GetPlayers()
        {
            var players = new SqlPlayerRepository(connectionString).RetrievePlayers();
            return players;
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> PutPlayer(string username, Player player)
        {
            if(player.Username != username)
            {
                return BadRequest();
            }

            var created = new SqlPlayerRepository(connectionString).CreatePlayer(
                player.Username, 
                player.ChesscomId,
                player.Avatar,
                player.Title,
                player.Status,
                player.Name);

            return NoContent();
        }


        public PlayerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
