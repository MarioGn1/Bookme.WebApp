using Bookme.ViewModels.Booking;
using System;
using System.Collections.Generic;

namespace Bookme.Services.Contracts
{
    public interface IBookingService
    {
        public SheduleViewModel GetDaySchedule(int serviceId, string dateString);
        public ServiceBookingViewModel GetServiceInfo(int serviceId, string clientId);
        public bool CreateBooking(BookServiceViewModel model);
        public BookingsByDayViewModel GetAllMyBookings(string clientId, DateTime date, int daysPerPage);
        public BusinessBookingsByDayViewModel GetMyBusinessBookings(string businessId, DateTime date, int daysPerPage);
        public IEnumerable<BookingHistoryViewModel> GetHistoryBookings(string userId);
    }
}
