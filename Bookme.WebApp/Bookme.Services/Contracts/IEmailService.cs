using Bookme.ViewModels.Booking;
using System.Threading.Tasks;

namespace Bookme.Services.Contracts
{
    public interface IEmailService
    {
        public Task<bool> SendEmailBookingConfirmation(BookServiceViewModel model);
    }
}
