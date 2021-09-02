namespace Bookme.Data.Models
{
    public class Raiting
    {
        public string VoterId { get; init; }
        public ApplicationUser Voter { get; set; }
        public int BusinessId { get; init; }
        public Business Business { get; set; }
        public bool IsLiked { get; set; }
    }
}
