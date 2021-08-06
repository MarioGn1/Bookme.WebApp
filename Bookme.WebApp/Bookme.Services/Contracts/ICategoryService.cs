using Bookme.ViewModels.Categories;
using System.Collections.Generic;

namespace Bookme.Services.Contracts
{
    public interface ICategoryService
    {
        public CategoryAllMembersViewModel GetCategoryMembers(
            int categoryId, 
            string searchByBusiness, 
            string searchByService,
            OfferedServiceSort sortCriteria = OfferedServiceSort.Rating,
            int currPage = 1, int membersPerPage = int.MaxValue);
    }
}
