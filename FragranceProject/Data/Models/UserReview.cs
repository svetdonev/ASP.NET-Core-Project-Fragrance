namespace FragranceProject.Data.Models
{
    public class UserReview
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
