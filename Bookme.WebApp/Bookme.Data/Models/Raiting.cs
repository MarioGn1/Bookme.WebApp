namespace Bookme.Data.Models
{
    public class Raiting
    {
        public string VoterId { get; init; }
        public ApplicationUser Voter { get; set; }
        public int CommentId { get; init; }
        public Comment Comment { get; set; }
        public bool IsLiked { get; set; }
    }
}
