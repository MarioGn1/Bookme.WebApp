using Bookme.ViewModels.Booking;
using System;

namespace Bookme.Services.Contracts
{
    public interface IBookingService
    {
        public SheduleViewModel GetDaySchedule(int serviceId, string dateString);
        public ServiceBookingViewModel GetServiceInfo(int serviceId, string clientId);
        public bool CreateBooking(BookServiceViewModel model);
        public BookingsByDayViewModel GetAllMyBookings(string clientId, DateTime date);
    }
}
