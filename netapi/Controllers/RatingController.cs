using Microsoft.AspNetCore.Mvc;
using netapi.Models;

namespace netapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        private const string connectionString = @"Server=DESKTOP-VT4KSCJ\SQLEXPRESS;Database=ChessLocal;Integrated Security=SSPI;Encrypt=False";

        [HttpPut("")]
        public async Task<IActionResult> PutPlayer(Rating rating)
        {
            Rating created = await Task.Run(() => new SqlRatingRepository(connectionString).CreateRating(
                rating.PlayerId,
                rating.Daily,
                rating.Daily960,
                rating.Rapid,
                rating.Bullet,
                rating.Blitz,
                rating.Tactics,
                rating.Fide)
            );

            return NoContent();
        }

        public RatingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
