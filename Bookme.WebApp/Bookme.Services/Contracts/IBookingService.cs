﻿using Bookme.ViewModels.Booking;

namespace Bookme.Services.Contracts
{
    public interface IBookingService
    {
        public SheduleViewModel GetDaySchedule(int serviceId, string dateString);
        public ServiceInfoViewModel GetServiceInfo(int serviceId);
    }
}