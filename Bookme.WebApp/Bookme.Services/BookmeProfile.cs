using AutoMapper;
using Bookme.Data.Models;
using Bookme.ViewModels.Booking;
using Bookme.ViewModels.BookingConfiguration;
using Bookme.ViewModels.Business;
using Bookme.ViewModels.Categories;
using Bookme.ViewModels.HomeModels;
using Bookme.ViewModels.OfferedServices;

namespace Bookme.Services
{
    public class BookmeProfile : Profile
    {
        public BookmeProfile()
        {
            this.CreateMap<ServiceCategory, ClientHomeViewModel>();
            this.CreateMap<ServiceCategory, ServiceCategoryViewModel>();

            this.CreateMap<VisitationType, VisitationTypeViewModel>();            

            this.CreateMap<WeeklyScheduleViewModel, WeeklySchedule>();
            this.CreateMap<WeeklySchedule, WeeklyScheduleViewModel>();
            this.CreateMap<PartialBreakViewModel, BreakTemplate>();

            this.CreateMap<Business, CategoryMemberViewModel>();
            this.CreateMap<Business, BusinessDetailsViewModel>();
            this.CreateMap<CreateBusinessViewModel, Business>();

            this.CreateMap<BreakTemplate, BreakViewModel>();

            this.CreateMap<OfferedService, ServiceBookingViewModel>()
                .ForMember(dto => dto.OwnerId, x => x.MapFrom(data => data.UserId))
                .ForMember(dto => dto.ServiceId, x => x.MapFrom(data => data.Id))
                .ForMember(dto => dto.OperatorName, x => x.MapFrom(data => data.User.FirstName + " " + data.User.LastName)); 
            this.CreateMap<OfferedService, OfferedServiceDetailsViewModel>()
                .ForMember(dto => dto.Category, x => x.MapFrom(data => data.ServiceCategory.Name));
            this.CreateMap<OfferedService, GetOfferedServiceViewModel>()
                .ForMember(dto => dto.Category, x => x.MapFrom(data => data.ServiceCategory.Name));

            this.CreateMap<BookServiceViewModel, Booking>()
                .ForMember(data => data.BusinessId, x => x.MapFrom(dto => dto.OwnerId))
                .ForMember(data => data.ClientFirstName, x => x.MapFrom(dto => dto.FirstName))
                .ForMember(data => data.ClientLastName, x => x.MapFrom(dto => dto.LastName))
                .ForMember(data => data.ClientPhoneNumber, x => x.MapFrom(dto => dto.PhoneNumber))
                .ForMember(data => data.BookedService, x => x.MapFrom(dto => dto.Name));

            this.CreateMap<Booking, BookingViewModel>()
                .ForMember(dto => dto.BusinessName, x => x.MapFrom(data => data.Business.FirstName + " " + data.Business.LastName))
                .ForMember(dto => dto.BusinessPhone, x => x.MapFrom(data => data.Business.Business.PhoneNumber));
        }
    }
}
