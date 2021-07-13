using System.ComponentModel.DataAnnotations;
using static Bookme.Data.DbConstants;

namespace Bookme.Data.Models
{
    public class VisitationType
    {      
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Type { get; set; }
        public decimal Price { get; set; } = DEFAULT_VISITATION_PRICE;
    }
}
