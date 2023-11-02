using Microsoft.Data.SqlClient;
using netapi.Models;
using System.Data;
using System.Transactions;

namespace netapi
{
    public class RatingRepository : IRatingRepository
    {
        private readonly string connectionString;

        public RatingRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public Rating CreateRating(int playerId, int? daily, int? daily960, int? rapid, int? bullet, int? blitz, int? tactics, int? fide)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Chesscom.CreatePlayerRating", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("PlayerId", playerId);
                        command.Parameters.AddWithValue("Daily", daily);
                        command.Parameters.AddWithValue("Daily960", daily960);
                        command.Parameters.AddWithValue("ChessRapid", rapid);
                        command.Parameters.AddWithValue("ChessBullet", bullet);
                        command.Parameters.AddWithValue("ChessBlitz", blitz);
                        command.Parameters.AddWithValue("Tactics", tactics);
                        command.Parameters.AddWithValue("Fide", fide);

                        var p = command.Parameters.Add("RatingId", SqlDbType.Int);
                        p.Direction = ParameterDirection.Output;

                        //var p = command.Parameters //might want to add an output to the procedure
                        connection.Open();
                        command.ExecuteNonQuery();
                        transaction.Complete();

                        var ratingId = (int)command.Parameters["RatingId"].Value;

                        return new Rating(ratingId, playerId, daily, daily960, rapid, bullet, blitz, tactics, fide);
                    }
                }
            }
        }

        public Rating GetRecentRating(int playerId)
        {
            throw new NotImplementedException();
        }

        public Rating GetRecentRating(string username)
        {
            throw new NotImplementedException();
        }
    }
}
