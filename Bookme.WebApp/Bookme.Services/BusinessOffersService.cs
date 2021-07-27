﻿using AutoMapper;
using Bookme.Data;
using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.Business;
using Bookme.ViewModels.OfferedServices;
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

        public bool CheckForCategory(int categoryId)
        {
            return this.data.ServiceCategories.Any(x => x.Id == categoryId);
        }

        public bool CheckForVisitationType(int visitationTypeId)
        {
            return this.data.VisitationTypes.Any(x => x.Id == visitationTypeId);
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
    }
}
