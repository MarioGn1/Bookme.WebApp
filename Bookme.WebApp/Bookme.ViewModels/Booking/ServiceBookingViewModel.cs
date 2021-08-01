using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.Booking
{
    public class ServiceBookingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Duration { get; set; }

        public string OwnerId { get; set; }
        [Display(Name = "Operated by:")]
        public string OperatorName { get; set; }

        public string ClientId { get; set; }
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number:")]
        public string PhoneNumber { get; set; }
    }
}
