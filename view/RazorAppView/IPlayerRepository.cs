using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace RazorAppView
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
        Player CreatePlayer(string username, int chesscomId, string? avatar, string? title, string? status, string? name);
    }
}
