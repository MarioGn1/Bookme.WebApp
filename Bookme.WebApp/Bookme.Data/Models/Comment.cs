using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [MaxLength(500)]
        public string Content { get; set; }
        public ICollection<Raiting> Raitings { get; set; }
    }
}
