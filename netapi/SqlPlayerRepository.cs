using Microsoft.Data.SqlClient;
using netapi.Models;
using System.Data;

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
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Chesscom.CreatePlayer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                        return TranslatePlayers(reader);
                }
            }
        }

        private IReadOnlyList<Player> TranslatePlayers(SqlDataReader reader)
        {
            var players = new List<Player>();

            //var playerIdOrd = reader.GetOrdinal("PlayerId");
            var usernameOrd = reader.GetOrdinal("Username");
            var chesscomIdOrd = reader.GetOrdinal("ChesscomId");
            var avatarOrd = reader.GetOrdinal("Avatar");
            var titleOrd = reader.GetOrdinal("Title");
            var statusOrd = reader.GetOrdinal("Status");
            var nameOrd = reader.GetOrdinal("Name");

            while(reader.Read())
            {
                players.Add(new Player(
                    reader.GetString(usernameOrd),
                    reader.GetInt32(chesscomIdOrd),
                    reader.GetString(avatarOrd),
                    reader.GetString(titleOrd),
                    reader.GetString(statusOrd),
                    reader.GetString(nameOrd)
                    ));
            }

            return players;
        }
    }
}
