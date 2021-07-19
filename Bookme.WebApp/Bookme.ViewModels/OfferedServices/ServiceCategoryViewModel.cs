using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.OfferedServices
{
    public class ServiceCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
