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

        [HttpGet()]
        public IReadOnlyList<Player> GetPlayers()
        {
            var players = new SqlPlayerRepository(@"Server=DESKTOP-VT4KSCJ\SQLEXPRESS;Database=ChessLocal;Integrated Security=SSPI;Encrypt=False").RetrievePlayers();
            return players;
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> PutPlayer(string username, Player player)
        {
            if(player.Username != username)
            {
                return BadRequest();
            }

            

            return NoContent();
        }


        public PlayerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
