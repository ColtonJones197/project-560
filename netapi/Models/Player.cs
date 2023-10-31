﻿namespace netapi.Models
{
    public class Player
    {
        public string Username { get; set; }
        public int ChesscomId { get; set; }
        public string? Avatar { get; set; }
        public string? Title { get; set; }
        public string? Status { get; set; }
        public string? Name { get; set; }

        public Player(string username, int chesscomId, string? avatar, string? title, string? status, string? name)
        {
            Username = username;
            ChesscomId = chesscomId;
            Avatar = avatar;
            Title = title;
            Status = status;
            Name = name;
        }
    }
}
