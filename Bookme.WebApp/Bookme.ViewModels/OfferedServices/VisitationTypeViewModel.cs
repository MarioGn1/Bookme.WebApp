using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.OfferedServices
{
    public class VisitationTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; init; }
    }
}
