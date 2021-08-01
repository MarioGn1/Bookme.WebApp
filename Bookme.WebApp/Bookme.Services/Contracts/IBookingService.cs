using Bookme.ViewModels.Booking;

namespace Bookme.Services.Contracts
{
    public interface IBookingService
    {
        public SheduleViewModel GetDaySchedule(int serviceId, string dateString);
        public ServiceBookingViewModel GetServiceInfo(int serviceId, string clientId);
        public void CreateBooking(BookServiceViewModel model);
    }
}
