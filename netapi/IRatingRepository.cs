using netapi.Models;

namespace netapi
{
    public interface IRatingRepository
    {
        Rating GetRecentRating(int playerId);

        Rating GetRecentRating(string username);
        Rating CreateRating(int playerId, int? daily, int? daily960, int? rapid, int? bullet, int? blitz, int? tactics, int? fide);

    }
}
