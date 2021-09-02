using AutoMapper;
using Bookme.Data;
using Bookme.Services.Contracts;
using Bookme.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bookme.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BookmeDbContext data;
        private readonly IMapper mapper;

        public CategoryService(BookmeDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public CategoryAllMembersViewModel GetCategoryMembers(
            int categoryId,
            string searchByBusiness = null,
            string searchByService = null,
            OfferedServiceSort sortCriteria = OfferedServiceSort.Rating,
            int currPage = 1,
            int membersPerPage = int.MaxValue)
        {

            var membersQuery = data.Users
                .Include(x => x.Business)
                .Where(x => x.OfferedServices.Any(x => x.ServiceCategoryId == categoryId));

            if (!string.IsNullOrWhiteSpace(searchByBusiness))
            {
                membersQuery = membersQuery
                    .Where(x => x.Business.BusinessName.ToLower().Contains(searchByBusiness.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(searchByService))
            {
                membersQuery = membersQuery
                    .Where(x => x.OfferedServices
                        .Any(x => (x.Name + ' ' + x.Description).ToLower().Contains(searchByService.ToLower())));
            }

            membersQuery = sortCriteria switch
            {
                OfferedServiceSort.Rating => membersQuery.OrderByDescending(u => u.Raitings.Where(r => r.IsLiked == true).Count()),
                OfferedServiceSort.DateCreatedOldestFirst => membersQuery.OrderBy(c => c.Id),
                OfferedServiceSort.DateCreatedNewestFirst or _ => membersQuery.OrderByDescending(c => c.Id)
            };

            var totalMembers = membersQuery.Count();

            var members = membersQuery
                .Skip((currPage - 1) * membersPerPage)
                .Take(membersPerPage)
                .Select(x => x.Business)                
                .ToList();

            var membersDtos = mapper.Map<IEnumerable<CategoryMemberViewModel>>(members);

            var model = new CategoryAllMembersViewModel
            {
                TotalMembers = totalMembers,
                CurrentPage = currPage,
                Members = membersDtos
            };

            return model;
        }
    }
}