using netapi.Models;

namespace netapi
{
    public interface IGameRepository
    {
        IReadOnlyCollection<Game> GetGamesByPlayer(string username);

        Game GetGameById(int gameId);

        Game CreateGame(string url, string whitePlayer, string blackPlayer, int result, string pgn, DateTime startTime,
            string timeControl, string rules, DateTime? endTime, int? tournamentId, string? opening);
    }
}
