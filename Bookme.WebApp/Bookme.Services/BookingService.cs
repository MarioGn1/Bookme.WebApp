using AutoMapper;
using Bookme.Data;
using Bookme.Services.Contracts;
using Bookme.ViewModels.Booking;
using Microsoft.EntityFrameworkCore;
using System;
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

            var serviceData = data.OfferedServices
                .Where(x => x.Id == serviceId)
                .Select(x => new
                {
                    x.UserId,
                    x.Duration
                })
                .FirstOrDefault();

            var userId = serviceData.UserId;
            var ownerInfo = GetOwnerInfo(userId);


            var bookedHours = data.Bookings
                .Where(x => x.Date == date && x.BusinessId == userId)
                .Select(x => x.Date)
                .ToList();

            var model = new SheduleViewModel
            {
                OwnerInfo = ownerInfo,
                bookedHours = bookedHours
            };

            return model;
        }

        public ServiceInfoViewModel GetServiceInfo(int serviceId)
        {
            var serviceInfo = data.OfferedServices
                .Include(x => x.User)
                .Where(x => x.Id == serviceId)
                .FirstOrDefault();

            var serviceInfoDto = mapper.Map<ServiceInfoViewModel>(serviceInfo);

            return serviceInfoDto;
        }

        private OwnerInfoViewModel GetOwnerInfo(string userId)
            => data.BusinessInfos
                .Where(x => x.UserId == userId)
                .Select(x => new OwnerInfoViewModel
                {
                    ServiceInterval = x.BookingConfiguration.ServiceInterval,
                    ShiftStart = x.BookingConfiguration.ShiftStart,
                    ShiftEnd = x.BookingConfiguration.ShiftEnd
                })
                .FirstOrDefault();
    }
}
