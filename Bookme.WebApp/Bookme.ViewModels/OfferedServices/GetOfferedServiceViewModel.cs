namespace Bookme.ViewModels.OfferedServices
{
    public class GetOfferedServiceViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
