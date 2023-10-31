using netapi.Models;

namespace netapi
{
    public interface IPlayerRepository
    {
        /// <summary>
        /// Retrieves all players within the database
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<Player> RetrievePlayers();
        Player GetPlayer(int ChesscomdId);
        Player GetPlayer(string Username);
        Player CreatePlayer(string username, uint chesscomId, string? avatar, string? title, string? status, string? name);
    }
}
