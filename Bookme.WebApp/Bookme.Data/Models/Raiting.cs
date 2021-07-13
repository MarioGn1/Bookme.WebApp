namespace Bookme.Data.Models
{
    public class Raiting
    {
        public string VoterId { get; set; }
        public ApplicationUser Voter { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public bool IsLiked { get; set; }
    }
}
