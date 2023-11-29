namespace netapi.Models
{
    public class Game
    {

        public int GameId { get; set; }
        public string url { get; set; }
        public string WhitePlayer {get; set;}
        public string BlackPlayer {get; set;}
        public int Result {get; set;}
        public string Pgn {get; set;}
        public DateTime StartTime {get; set;}
        public DateTime? EndTime {get; set;}
        public string TimeControl {get; set;}
        public string Rules {get; set;}
        public int TournamentId {get; set;}

        public Game()
        {
            
        }
     }
}
