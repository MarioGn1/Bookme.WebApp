using Bookme.ViewModels.BookingConfiguration;
using System.Threading.Tasks;

namespace Bookme.Services.Contracts
{
    public interface IBookingConfigurationService
    {
        public Task<bool> CreateBookingConfiguration(ConfigureBookingConfigurationViewModel model, string userId);
        public ConfigureBookingConfigurationViewModel GetBookingConfigurationInfo(string userId);
        public bool EditBookingConfiguration(ConfigureBookingConfigurationViewModel model, string userId);
    }
}
