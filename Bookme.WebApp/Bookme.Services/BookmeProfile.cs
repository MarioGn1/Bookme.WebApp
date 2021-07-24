using AutoMapper;
using Bookme.Data.Models;
using Bookme.ViewModels.Business;
using Bookme.ViewModels.HomeModels;
using Bookme.ViewModels.OfferedServices;

namespace Bookme.Services
{
    public class BookmeProfile : Profile
    {
        public BookmeProfile()
        {
            this.CreateMap<ServiceCategory, ServiceCategoryViewModel>();
            this.CreateMap<VisitationType, VisitationTypeViewModel>();
            this.CreateMap<OfferedService, MyOfferedServiceViewModel>()
                .ForMember(dto => dto.Category, x => x.MapFrom(data => data.ServiceCategory.Name));

            this.CreateMap<ServiceCategory, ClientHomeViewModel>();

            this.CreateMap<CreateBusinessViewModel, Business>();
        }
    }
}
