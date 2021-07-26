using Bookme.ViewModels.Categories;
using System.Collections.Generic;

namespace Bookme.Services.Contracts
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryMemberViewModel> GetCategoryMembers(int categoryId);
    }
}
