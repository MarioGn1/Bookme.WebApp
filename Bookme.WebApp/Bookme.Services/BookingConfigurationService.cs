using AutoMapper;
using Bookme.Data;
using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.BookingConfiguration;
using Microsoft.EntityFrameworkCore;
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

        public ConfigureBookingConfigurationViewModel GetBookingConfigurationInfo(string userId)
        {
            var config = data.Businesses
                .Include(x => x.BookingConfiguration)
                .ThenInclude(x => x.WeeklySchedule)
                .Include(x => x.BookingConfiguration)
                .ThenInclude(x => x.Breaks)
                .ToList()
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            var breaksDTO = new BreaksViewModel();

            foreach (var breakTemplate in config.BookingConfiguration.Breaks)
            {
                var currPartial = new PartialBreakViewModel { BreakStart = breakTemplate.BreakStart, BreakEnd = breakTemplate.BreakEnd };
                if (breaksDTO.FirstBreak == null)
                {
                    breaksDTO.FirstBreak = currPartial;
                    continue;
                }
                if (breaksDTO.FirstBreak != null && breaksDTO.SecondBreak == null)
                {
                    breaksDTO.SecondBreak = currPartial;
                    continue;
                }
                if (breaksDTO.FirstBreak != null && breaksDTO.SecondBreak != null && breaksDTO.ThirdBreak == null)
                {
                    breaksDTO.ThirdBreak = currPartial;
                }
            }

            var dto = new ConfigureBookingConfigurationViewModel
            {
                Id = config.BookingConfigurationId,
                ShiftStart = config.BookingConfiguration.ShiftStart,
                ShiftEnd = config.BookingConfiguration.ShiftEnd,
                ServiceInterval = config.BookingConfiguration.ServiceInterval,
                WeeklySchedule = mapper.Map<WeeklyScheduleViewModel>(config.BookingConfiguration.WeeklySchedule),
                Breaks = breaksDTO
            };

            return dto;
        }

        public async Task<bool> CreateBookingConfiguration(ConfigureBookingConfigurationViewModel model, string userId)
        {
            var business = data.Businesses
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

        public bool EditBookingConfiguration(ConfigureBookingConfigurationViewModel model, string userId)
        {
            var bookingConfiguration = data.BookingConfigurations
                .Include(x => x.Business)
                .Include(x => x.WeeklySchedule)
                .Include(x => x.Breaks)
                .Where(x => x.Business.UserId == userId && x.Id == model.Id)
                .FirstOrDefault();

            if (bookingConfiguration == null)
            {
                return false;
            }

            var weeklySchedule = mapper.Map<WeeklySchedule>(model.WeeklySchedule);
            var breaks = CreateBreaks(model.Breaks);

            bookingConfiguration.ServiceInterval = model.ServiceInterval;
            bookingConfiguration.ShiftStart = model.ShiftStart;
            bookingConfiguration.ShiftEnd = model.ShiftEnd;
            bookingConfiguration.WeeklySchedule = weeklySchedule;
            bookingConfiguration.Breaks = breaks;

            data.SaveChanges();

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
