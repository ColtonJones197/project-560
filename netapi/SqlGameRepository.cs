using Microsoft.Data.SqlClient;
using netapi.Models;
using System.Data;
using System.Transactions;

namespace netapi
{
    public class SqlGameRepository : IGameRepository
    {
        private readonly string connectionString;

        public SqlGameRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Game CreateGame(string url, string whitePlayer, string blackPlayer, int result, string pgn, DateTime startTime, string timeControl, string rules, DateTime? endTime, int? tournamentId, string? opening)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Chesscom.CreateGame", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("Url", url);
                        command.Parameters.AddWithValue("WhitePlayerUsername", whitePlayer);
                        command.Parameters.AddWithValue("BlackPlayerUsername", blackPlayer);
                        command.Parameters.AddWithValue("Result", result);
                        command.Parameters.AddWithValue("PGN", pgn);
                        command.Parameters.AddWithValue("EcoOpening", opening);
                        command.Parameters.AddWithValue("StartTime", startTime);
                        command.Parameters.AddWithValue("EndTime", endTime);
                        command.Parameters.AddWithValue("TimeControl", timeControl);
                        command.Parameters.AddWithValue("Rules", rules);
                        command.Parameters.AddWithValue("TournamentUrl", tournamentId);

                        var p = command.Parameters.Add("GameId", SqlDbType.Int);
                        p.Direction = ParameterDirection.Output;

                        //var p = command.Parameters //might want to add an output to the procedure
                        connection.Open();
                        command.ExecuteNonQuery();
                        transaction.Complete();

                        var gameId = (int)command.Parameters["GameId"].Value;

                        return new Game(gameId, url, whitePlayer, blackPlayer, result, pgn, startTime, timeControl,
                            rules, tournamentId, endTime, opening);
                    }
                }
            }
        }

        public Game GetGameById(int gameId)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Game> GetGamesByPlayer(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Chesscom.RetrieveGamesByUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("Username", username);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                        return TranslateGames(reader);
                }
            }
        }

        private IReadOnlyList<Game> TranslateGames(SqlDataReader reader)
        {
            var games = new List<Game>();

            var gameIdOrd = reader.GetOrdinal("GameId");
            var urlOrd = reader.GetOrdinal("Url");
            var whiteOrd = reader.GetOrdinal("WhiteUsername");
            var blackOrd = reader.GetOrdinal("BlackUsername");
            var resultOrd = reader.GetOrdinal("Result");
            var pgnOrd = reader.GetOrdinal("PGN");
            var openingOrd = reader.GetOrdinal("EcoOpening");
            var startOrd = reader.GetOrdinal("StartTime");
            var endOrd = reader.GetOrdinal("EndTime");
            var timeControlOrd = reader.GetOrdinal("TimeControl");
            var rulesOrd = reader.GetOrdinal("Rules");
            var tournamentIdOrd = reader.GetOrdinal("TournamentId");

            while (reader.Read())
            {
                DateTime? end = null;
                int? tournamentId = null;

                if (!reader.IsDBNull(endOrd)) end = reader.GetDateTime(endOrd);
                if (!reader.IsDBNull(tournamentIdOrd)) tournamentId = reader.GetInt32(tournamentIdOrd);
                var opening = !reader.IsDBNull(openingOrd) ? reader.GetString(openingOrd) : null;

                games.Add(new Game(
                    reader.GetInt32(gameIdOrd),
                    reader.GetString(urlOrd),
                    reader.GetString(whiteOrd),
                    reader.GetString(blackOrd),
                    reader.GetInt32(resultOrd),
                    reader.GetString(pgnOrd),
                    reader.GetDateTime(startOrd),
                    reader.GetString(timeControlOrd),
                    reader.GetString(rulesOrd),
                    tournamentId,
                    end,
                    opening
                    ));
            }

            return games;
        }
    }
}
