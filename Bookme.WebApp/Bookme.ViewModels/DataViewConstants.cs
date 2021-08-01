namespace Bookme.ViewModels
{
    public class DataViewConstants
    {
        public class OfferedService
        {
            public const int SERVICE_NAME_MIN_LENGTH = 4;
            public const int SERVICE_NAME_MAX_LENGTH = 50;
            public const int SERVICE_DESCRIPTION_MIN_LENGTH = 10;
            public const int SERVICE_DESCRIPTION_MAX_LENGTH = 1000;
            public const double SERVICE_MIN_PRICE = 0;
            public const double SERVICE_MAX_PRICE = double.MaxValue;
            public const int SERVICE_DURATION_MIN_TIME = 0;
            public const int SERVICE_DURATION_MAX_TIME = 600;
        }

        public class Business
        {
            public const int NAME_MIN_LENGTH = 3;
            public const int NAME_MAX_LENGTH = 50;
            public const int DESCRIPTION_MIN_LENGTH = 10;
            public const int DESCRIPTION_MAX_LENGTH = 1000;
            public const int LOCATION_MIN_LENGTH = 6;
            public const int LOCATION_MAX_LENGTH = 200;
            public const int PHONE_MIN_LENGTH = 6;
            public const int PHONE_MAX_LENGTH = 20;
            public const int ADDRESS_MIN_LENGTH = 6;
            public const int ADDRESS_MAX_LENGTH = 50;
        }

        public class BookingConfiguration
        {
            public const int SERVICE_MIN_DURATION = 0;
            public const int SERVICE_MAX_DURATION = 720;
        }

        public class Register
        {
            public const int NAME_MIN_LENGTH = 3;
            public const int NAME_MAX_LENGTH = 30;
            public const int PASSWORD_MIN_LENGTH = 6;
            public const int PASSWORD_MAX_LENGTH = 100;
        }
    }
}
