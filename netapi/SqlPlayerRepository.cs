using netapi.Models;

namespace netapi
{
    public class SqlPlayerRepository : IPlayerRepository
    {

        private readonly string connectionString;

        public SqlPlayerRepository(string connectionString) 
        {
            this.connectionString = connectionString;
        }

        public Player CreatePlayer(string username, uint chesscomId, string? avatar, string? title, string? status, string? name)
        {
            throw new NotImplementedException();
        }

        public Player GetPlayer(int ChesscomdId)
        {
            throw new NotImplementedException();
        }

        public Player GetPlayer(string Username)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Player> RetrievePlayers()
        {
            throw new NotImplementedException();
        }
    }
}
