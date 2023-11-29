namespace netapi.Models
{
    public class Game
    {

        public int GameId { get; set; }
        public string Url { get; set; }
        public string WhitePlayer {get; set;}
        public string BlackPlayer {get; set;}
        public int Result {get; set;}
        public string Pgn {get; set;}
        
        public string? Opening { get; set; }
        public DateTime StartTime {get; set;}
        public DateTime? EndTime {get; set;}
        public string TimeControl {get; set;}
        public string Rules {get; set;}
        public int? TournamentId {get; set;}

        public Game(int gameId, string url, string white, string black, int result, string pgn, DateTime start, string timeControl, string rules, int? tournamentId, DateTime? end, string? opening)
        {
            GameId = gameId;
            Url = url;
            WhitePlayer = white;
            BlackPlayer = black;
            Result = result;
            Pgn = pgn;
            StartTime = start;
            EndTime = end;
            TimeControl = timeControl;
            Rules = rules;
            TournamentId = tournamentId;
            Opening = opening;
        }
     }
}
