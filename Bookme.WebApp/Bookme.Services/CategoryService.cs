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

        public IEnumerable<CategoryMemberViewModel> GetCategoryMembers(int categoryId)
        {

            var members = data.Users
                .Include(x => x.Business)
                .Where(x => x.OfferedServices.Any(x => x.ServiceCategoryId == categoryId))
                .Select(x => x.Id)
                .ToList();

            var memberInfo = data.BusinessInfos.Where(x => members.Contains(x.UserId)).ToList();

            var membersDtos = mapper.Map<IEnumerable<CategoryMemberViewModel>>(memberInfo);

            return membersDtos;
        }
    }
}
