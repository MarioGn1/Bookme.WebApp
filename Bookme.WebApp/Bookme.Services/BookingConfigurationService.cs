using AutoMapper;
using Bookme.Data;
using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.BookingConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookme.Services 
{
    public class BookingConfigurationService : IBookingConfigurationService
    {
        private readonly BookmeDbContext data;
        private readonly IMapper mapper;

        public BookingConfigurationService(BookmeDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<bool> CreateBookingConfiguration(ConfigureBookingConfigurationViewModel model, string userId)
        {
            var business = data.BusinessInfos
                .Where(x => x.UserId == userId)
                .FirstOrDefault();
            
            //If business already have configuration
            if (business.BookingConfigurationId != null)
            {
                return false;
            }

            var weeklySchedule = mapper.Map<WeeklySchedule>(model.WeeklySchedule);
            var breaks = CreateBreaks(model.Breaks);

            var bookingConfiguration = new BookingConfiguration
            {
                ServiceInterval = model.ServiceInterval,
                ShiftStart = model.ShiftStart,
                ShiftEnd = model.ShiftEnd,
                WeeklySchedule = weeklySchedule,
                Breaks = breaks
            };

            data.BookingConfigurations.Add(bookingConfiguration);

            business.BookingConfiguration = bookingConfiguration;

            await data.SaveChangesAsync();

            return true;
        }

        private ICollection<BreakTemplate> CreateBreaks(BreaksViewModel breaksModel)
        {
            var breaks = new List<BreakTemplate>();

            var breaksDtos = new List<PartialBreakViewModel>();
            breaksDtos.Add(breaksModel.FirstBreak);
            breaksDtos.Add(breaksModel.SecondBreak);
            breaksDtos.Add(breaksModel.ThirdBreak);

            TimeSpan validationTime = TimeSpan.Parse("00:00:00");

            foreach (var shiftBreak in breaksDtos)
            {
                if (shiftBreak.BreakStart == validationTime && shiftBreak.BreakEnd == validationTime)
                {
                    continue;
                }
                if (shiftBreak.BreakStart == shiftBreak.BreakEnd)
                {
                    continue;
                }
                breaks.Add(mapper.Map<BreakTemplate>(shiftBreak));
            }

            return breaks;
        }
    }
}
