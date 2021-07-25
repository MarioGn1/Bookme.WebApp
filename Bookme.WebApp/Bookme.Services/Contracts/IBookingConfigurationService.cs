using Bookme.ViewModels.BookingConfiguration;

namespace Bookme.Services.Contracts
{
    public interface IBookingConfigurationService
    {
        public bool CreateBookingConfiguration(ConfigureBookingConfigurationViewModel model, string userId);
    }
}
