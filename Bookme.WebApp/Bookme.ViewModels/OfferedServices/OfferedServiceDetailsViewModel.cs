using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.OfferedServices
{
    public class OfferedServiceDetailsViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Display(Name = "Price:")]
        public double Price { get; set; }

        [Display(Name = "Visitation Price:")]
        public double VisitationPrice { get; set; }

        [Display(Name = "Duration:")]
        public int? Duration { get; set; }

        [Display(Name = "Category:")]
        public string Category { get; set; }

        [Display(Name = "Visitation Types:")]
        public string VisitationTypes { get; set; }

        public string ImageUrl { get; set; }
    }
}
