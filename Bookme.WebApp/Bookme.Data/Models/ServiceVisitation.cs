namespace Bookme.Data.Models
{
    public class ServiceVisitation
    {
        public int OfferedServiceId { get; set; }
        public OfferedService OfferedService { get; set; }
        public int VisitationTypeId { get; set; }
        public VisitationType VisitationType { get; set; }
    }
}
