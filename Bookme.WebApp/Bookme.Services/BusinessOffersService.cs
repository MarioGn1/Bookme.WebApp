using AutoMapper;
using Bookme.Data;
using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.Business;
using Bookme.ViewModels.OfferedServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bookme.Services
{
    public class BusinessOffersService : IBusinessOffersService
    {
        private readonly BookmeDbContext data;
        private readonly IMapper mapper;

        public BusinessOffersService(BookmeDbContext dbContext, IMapper mapper)
        {
            this.data = dbContext;
            this.mapper = mapper;
        }

        public IEnumerable<ServiceCategoryViewModel> GetAllCategories()
        {
            var categories = data.ServiceCategories
                .ToList();

            var categoriesDto = mapper.Map<IEnumerable<ServiceCategoryViewModel>>(categories);

            return categoriesDto;
        }

        public IEnumerable<VisitationTypeViewModel> GetAllVisitationTypes()
        {
            var visitationTypes = data.VisitationTypes
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

        public BusinessDetailsViewModel GetBusinesDetails(string userId)
        {
            var businessInfo = data.BusinessInfos
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            var businessServicess = this.GetAllBusinessServices(userId);

            var businessDetails = mapper.Map<BusinessDetailsViewModel>(businessInfo);
            businessDetails.offeredServices = businessServicess;

            return businessDetails;
        }

        public OfferedServiceDetailsViewModel GetServiceDetails(int serviceId)
        {
            var service = data.OfferedServices
                .Include(x => x.ServiceCategory)
                .Where(x => x.Id == serviceId)
                .FirstOrDefault();

            var visitations = data.ServiceVisitations
                .Include(x => x.VisitationType)
                .Where(x => x.OfferedServiceId == serviceId)
                .Select(x => x.VisitationType.Type)
                .ToList();

            var serviceDto = mapper.Map<OfferedServiceDetailsViewModel>(service);
            serviceDto.VisitationTypes = string.Join(",", visitations);

            return serviceDto;
        }

        public AddOfferedServiceViewModel GetEditViewModel(int serviceId)
        {
            var service = data.OfferedServices
                .Include(x => x.ServiceVisitations)
                .Where(x => x.Id == serviceId)
                .FirstOrDefault();

            var serviceDto = new AddOfferedServiceViewModel
            {
                Id = service.Id,
                OfferedService = new OfferedServiceViewModel
                {
                    Name = service.Name,
                    Price = (double)service.Price,
                    Description = service.Description,
                    ImageUrl = service.ImageUrl,
                    Duration = service.Duration,
                    ServiceCategoryId = service.ServiceCategoryId,
                    ServiceVisitationId = service.ServiceVisitations.FirstOrDefault().VisitationTypeId,
                    VisitationPrice = (double)service.VisitationPrice
                }
            };

            serviceDto.Categories = this.GetAllCategories();
            serviceDto.VisitationTypes = this.GetAllVisitationTypes();

            return serviceDto;
        }

        public IEnumerable<GetOfferedServiceViewModel> GetAllBusinessServices(string userId)
        {
            var allServices = data.OfferedServices
                .Where(x => x.UserId == userId)
                .Select(x => new OfferedService
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.Name,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    ServiceCategory = x.ServiceCategory
                })
                .ToList();

            var allservicesDtos = mapper.Map<IEnumerable<GetOfferedServiceViewModel>>(allServices);

            return allservicesDtos;
        }

        public bool CheckForCategory(int categoryId)
        {
            return this.data.ServiceCategories.Any(x => x.Id == categoryId);
        }

        public bool CheckForVisitationType(int visitationTypeId)
        {
            return this.data.VisitationTypes.Any(x => x.Id == visitationTypeId);
        }

        public bool CheckOwnership(int serviceId, string currentUserId)
        {
            var serviceUserId = data.OfferedServices
                .Where(x => x.Id == serviceId)
                .Select(x => x.UserId)
                .FirstOrDefault();

            return serviceUserId == currentUserId;
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

            data.OfferedServices.Add(offeredService);
            data.SaveChanges();
        }

        public void EditOfferedService(AddOfferedServiceViewModel model, int serviceId)
        {
            var service = data.OfferedServices
                .Include(x => x.ServiceVisitations)
                .Where(x => x.Id == serviceId)
                .FirstOrDefault();

            service.Name = model.OfferedService.Name;
            service.Description = model.OfferedService.Description;
            service.ImageUrl = model.OfferedService.ImageUrl;
            service.Duration = model.OfferedService.Duration;
            service.ServiceCategoryId = model.OfferedService.ServiceCategoryId;
            service.Price = (decimal)model.OfferedService.Price;
            service.VisitationPrice = (decimal)model.OfferedService.VisitationPrice;

            var defaultVisitation = service.ServiceVisitations.FirstOrDefault();
            var serviceVisitation = new ServiceVisitation { VisitationTypeId = model.OfferedService.ServiceVisitationId };
            service.ServiceVisitations.Remove(defaultVisitation);
            service.ServiceVisitations.Add(serviceVisitation);

            data.SaveChanges();
        }

        public void DeleteService(int serviceId)
        {
            var service = data.OfferedServices.Find(serviceId);

            data.OfferedServices.Remove(service);

            data.SaveChanges();
        }


    }
}
