namespace Bookme.ViewModels.Business
{
    public abstract class BusinessDetailsAbstract
    {
        public virtual string BusinessName { get; init; }
        public virtual string Description { get; init; }
        public virtual string SupportedLocation { get; init; }
        public virtual string Address { get; init; }
        public virtual string PhoneNumber { get; init; }
        public virtual string ImageUrl { get; init; }
    }
}
