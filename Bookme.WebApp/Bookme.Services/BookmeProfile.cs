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
            this.CreateMap<PartialBreakViewModel, BreakTemplate>();

            this.CreateMap<Business, CategoryMemberViewModel>();
            this.CreateMap<Business, BusinessDetailsViewModel>();
            this.CreateMap<CreateBusinessViewModel, Business>();

            this.CreateMap<OfferedService, ServiceInfoViewModel>()
                .ForMember(dto => dto.OperatorName, x => x.MapFrom(data => data.User.FirstName + " " + data.User.LastName)); ;
            this.CreateMap<OfferedService, OfferedServiceDetailsViewModel>()
                .ForMember(dto => dto.Category, x => x.MapFrom(data => data.ServiceCategory.Name));
            this.CreateMap<OfferedService, GetOfferedServiceViewModel>()
                .ForMember(dto => dto.Category, x => x.MapFrom(data => data.ServiceCategory.Name));
        }
    }
}
