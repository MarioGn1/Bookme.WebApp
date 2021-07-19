using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.OfferedServices
{
    public class ServiceCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; init; }
    }
}
