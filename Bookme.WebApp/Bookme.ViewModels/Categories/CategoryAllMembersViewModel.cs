using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.Categories
{
    public class CategoryAllMembersViewModel
    {
        public int Id { get; set; }
        public const int BusinessesPerPage = 3;
        public int CurrentPage { get; set; } = 1;
        public int TotalMembers { get; set; }
        [Display(Name = "Search by business name:")]
        public string BusinessName { get; set; }
        [Display(Name = "Search by service name:")]
        public string ServiceName { get; set; }
        [Display(Name = "Show results in order by:")]
        public OfferedServiceSort SortCriteria { get; set; }
        public IEnumerable<CategoryMemberViewModel> Members { get; set; }
    }
}
