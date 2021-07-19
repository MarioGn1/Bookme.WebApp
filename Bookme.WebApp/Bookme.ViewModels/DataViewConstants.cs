namespace Bookme.ViewModels
{
    public class DataViewConstants
    {
        public class OfferedService
        {
            public const int SERVICE_NAME_MIN_LENGTH = 4;
            public const int SERVICE_NAME_MAX_LENGTH = 4;
            public const int SERVICE_DESCRIPTION_MIN_LENGTH = 4;
            public const int SERVICE_DESCRIPTION_MAX_LENGTH = 4;
            public const double SERVICE_MIN_PRICE = 0;
            public const double SERVICE_MAX_PRICE = double.MaxValue;
            public const int SERVICE_DURATION_MIN_TIME = 0;
            public const int SERVICE_DURATION_MAX_TIME = 600;
        }
    }
}
