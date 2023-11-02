namespace netapi.Models
{
    public class Rating
    {

        public int RatingId { get; set; }
        public int PlayerId { get; set; }
        public int? Daily { get; set; }
        public int? Daily960 { get; set; }
        public int? Rapid { get; set; }
        public int? Bullet { get; set; }
        public int? Blitz { get; set; }
        public int? Tactics { get; set; }
        public int? Fide { get; set; }

        public DateTime? CreatedOn { get; set; } = null;

        public Rating(int ratingId, int playerId, int? daily, int? daily960, int? rapid, int? bullet, int? blitz, int? tactics, int? fide)
        {
            RatingId = ratingId;
            PlayerId = playerId;
            Daily = daily;
            Daily960 = daily960;
            Rapid = rapid;
            Bullet = bullet;
            Blitz = blitz;
            Tactics = tactics;
            Fide = fide;
        }
     }
}
