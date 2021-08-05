using AutoMapper;
using Bookme.Data;
using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.Booking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookme.Services
{
    public class BookingService : IBookingService
    {
        private readonly BookmeDbContext data;
        private readonly IMapper mapper;

        public BookingService(BookmeDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public SheduleViewModel GetDaySchedule(int serviceId, string dateString)
        {
            DateTime date = DateTime.Parse(dateString);
            
            var userId = data.OfferedServices
                .Where(x => x.Id == serviceId)
                .Select(x => x.UserId
                )
                .FirstOrDefault();

            var model = new SheduleViewModel();

            var isWorkingDay = CheckForDayOff(date, userId);
            model.IsWorkingDay = isWorkingDay;

            if (!isWorkingDay)
            {            
                return model;
            }

            var ownerInfo = GetOwnerInfo(userId);

            var bookedHours = data.Bookings
                .Where(x => x.Date.Date == date.Date && x.BusinessId == userId)
                .Select(x => new BookedHourViewModel
                {
                   Date = x.Date,
                   Duration = x.Duration
                })
                .OrderBy(x => x.Date)
                .ToList();

            model.OwnerInfo = ownerInfo;
            model.bookedHours = bookedHours;            

            return model;
        }

        public ServiceBookingViewModel GetServiceInfo(int serviceId, string clientId)
        {
            var serviceInfo = data.OfferedServices
                .Include(x => x.User)
                .Where(x => x.Id == serviceId)
                .FirstOrDefault();

            var clientData = data.Users
                .Where(x => x.Id == clientId)
                .Select(x => new
                {
                    ClientId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber
                })
                .FirstOrDefault();

            var serviceInfoDto = mapper.Map<ServiceBookingViewModel>(serviceInfo);
            serviceInfoDto.ClientId = clientData.ClientId;
            serviceInfoDto.FirstName = clientData.FirstName;
            serviceInfoDto.LastName = clientData.LastName;
            serviceInfoDto.PhoneNumber = clientData.PhoneNumber;

            return serviceInfoDto;
        }

        private OwnerInfoViewModel GetOwnerInfo(string userId)
            => data.BusinessInfos
                .Include(x => x.BookingConfiguration)
                .ThenInclude(x => x.Breaks)
                .Where(x => x.UserId == userId)
                .Select(x => new OwnerInfoViewModel
                {
                    ServiceInterval = x.BookingConfiguration.ServiceInterval,
                    ShiftStart = x.BookingConfiguration.ShiftStart,
                    ShiftEnd = x.BookingConfiguration.ShiftEnd,
                    Breaks = mapper.Map<ICollection<BreakViewModel>>(x.BookingConfiguration.Breaks)
                })
                .FirstOrDefault();

        public void CreateBooking(BookServiceViewModel model)
        {
            var booking = mapper.Map<Booking>(model);

            data.Bookings.Add(booking);
            data.SaveChanges();
        }


        private bool CheckForDayOff(DateTime date, string userId)
        {
            var dayOfWeek = date.DayOfWeek;

            var weeklySchedule = data.BusinessInfos
                .Include(x => x.BookingConfiguration)
                .ThenInclude(x => x.WeeklySchedule)
                .Where(x => x.UserId == userId)
                .Select(x => x.BookingConfiguration.WeeklySchedule)
                .FirstOrDefault();

            var dayProperty = weeklySchedule.GetType().GetProperty(dayOfWeek.ToString()).GetValue(weeklySchedule);

            return (bool)dayProperty;
        }
    }
}
