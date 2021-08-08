using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Bookme.Data.DbConstants.Comment;

namespace Bookme.Data.Models
{
    public class Comment
    {
        public int Id { get; init; }
        [Required]
        public string AuthorId { get; set; }
        public ApplicationUser Autor { get; set; }
        public int BusinessInfoId { get; set; }
        public Business BusinessInfo { get; set; }
        [Required]
        [MaxLength(CONTENT_MAX_LENGTH)]
        public string Content { get; set; }
        public ICollection<Raiting> Raitings { get; set; }
    }
}
