﻿using Microsoft.Data.SqlClient;
using netapi.Models;
using System.Data;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Transactions;

namespace netapi
{
    public class SqlPlayerRepository : IPlayerRepository
    {

        private readonly string connectionString;

        public SqlPlayerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Player? CreatePlayer(string username, int chesscomId, string? avatar, string? title, string? status, string? name)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The parameter cannot be null or empty", nameof(username));

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Chesscom.CreatePlayer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("Username", username);
                        command.Parameters.AddWithValue("ChesscomId", chesscomId);
                        command.Parameters.AddWithValue("Avatar", avatar);
                        command.Parameters.AddWithValue("Title", title);
                        command.Parameters.AddWithValue("Status", status);
                        command.Parameters.AddWithValue("Name", name);

                        var p = command.Parameters.Add("PlayerId", SqlDbType.Int);
                        p.Direction = ParameterDirection.Output;

                        //var p = command.Parameters //might want to add an output to the procedure
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            transaction.Complete();
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                            return null;
                        }

                        var playerId = (int)command.Parameters["PlayerId"].Value;

                        return new Player(playerId, username, chesscomId, avatar, title, status, name);
                    }
                }
            }
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
                using (var command = new SqlCommand("Chesscom.RetrievePlayers", connection))
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

            var playerIdOrd = reader.GetOrdinal("PlayerId");
            var usernameOrd = reader.GetOrdinal("Username");
            var chesscomIdOrd = reader.GetOrdinal("ChesscomId");
            var avatarOrd = reader.GetOrdinal("Avatar");
            var titleOrd = reader.GetOrdinal("Title");
            var statusOrd = reader.GetOrdinal("Status");
            var nameOrd = reader.GetOrdinal("Name");

            while (reader.Read())
            {
                var avatar = !reader.IsDBNull(avatarOrd) ? reader.GetString(avatarOrd) : null;
                var title = !reader.IsDBNull(titleOrd) ? reader.GetString(titleOrd) : null;
                var status = !reader.IsDBNull(statusOrd) ? reader.GetString(statusOrd) : null;
                var name = !reader.IsDBNull(nameOrd) ? reader.GetString(nameOrd) : null;
                players.Add(new Player(
                    reader.GetInt32(playerIdOrd),
                    reader.GetString(usernameOrd),
                    reader.GetInt32(chesscomIdOrd),
                    avatar,
                    title,
                    status,
                    name
                    ));
            }

            return players;
        }
    }
}
