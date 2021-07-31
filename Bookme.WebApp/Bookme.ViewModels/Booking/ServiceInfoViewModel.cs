using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.Booking
{
    public class ServiceInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Operated by:")]
        public string OperatorName { get; set; }
    }
}
