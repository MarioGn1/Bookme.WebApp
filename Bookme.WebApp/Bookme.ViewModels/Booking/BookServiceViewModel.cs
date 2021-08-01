using System;

namespace Bookme.ViewModels.Booking
{
    public class BookServiceViewModel
    {
        public int ServiceId { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public string ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Date { get; set; }
        public string Notes { get; set; }
    }
}
