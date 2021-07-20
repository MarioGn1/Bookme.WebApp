using AutoMapper;
using Bookme.Data;
using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.OfferedServices;
using System.Collections.Generic;
using System.Linq;

namespace Bookme.Services
{
    public class BusinessOffersService : IBusinessOffersService
    {
        private readonly BookmeDbContext context;
        private readonly IMapper mapper;

        public BusinessOffersService(BookmeDbContext dbContext, IMapper mapper)
        {
            this.context = dbContext;
            this.mapper = mapper;
        }

        public IEnumerable<ServiceCategoryViewModel> GetAllCategories()
        {
            var categories = context.ServiceCategories
                .ToList();

            var categoriesDto = mapper.Map<IEnumerable<ServiceCategoryViewModel>>(categories);

            return categoriesDto;
        }

        public IEnumerable<VisitationTypeViewModel> GetAllVisitationTypes()
        {
            var visitationTypes = context.VisitationTypes
                .ToList();

            var visitationTypesDtos = mapper.Map<IEnumerable<VisitationTypeViewModel>>(visitationTypes);

            return visitationTypesDtos;
        }

        public AddOfferedServiceViewModel GetAddViewModel()
        {
            var model = new AddOfferedServiceViewModel
            {
                Categories = this.GetAllCategories(),
                VisitationTypes = this.GetAllVisitationTypes()
            };

            return model;
        }

        public bool CheckForCategory(int categoryId)
        {
            return this.context.ServiceCategories.Any(x => x.Id == categoryId);
        }

        public bool CheckForVisitationType(int visitationTypeId)
        {
            return this.context.VisitationTypes.Any(x => x.Id == visitationTypeId);
        }

        public void CreateOfferedService(AddOfferedServiceViewModel model, string userId)
        {
            var offeredService = new OfferedService
            {
                Name = model.OfferedService.Name,
                Description = model.OfferedService.Description,
                UserId = userId,
                ImageUrl = model.OfferedService.ImageUrl,
                Duration = model.OfferedService.Duration,
                ServiceCategoryId = model.OfferedService.ServiceCategoryId,
                Price = (decimal)model.OfferedService.Price,
                VisitationPrice = (decimal)model.OfferedService.VisitationPrice
            };

            var serviceVisitation = new ServiceVisitation { VisitationTypeId = model.OfferedService.ServiceVisitationId };
            offeredService.ServiceVisitations.Add(serviceVisitation);

            context.OfferedServices.Add(offeredService);
            context.SaveChanges();
        }
    }
}
