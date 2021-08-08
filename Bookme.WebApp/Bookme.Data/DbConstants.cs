namespace Bookme.Data
{
    static class DbConstants
    {
        public class ApplicationUser
        {
            public const int NAME_MAX_LENGTH = 30;
        }

        public class Booking
        {
            public const int NAME_MAX_LENGTH = 50;
            public const int SERVICE_NAME_MAX_LENGTH = 50;
            public const int PHONE_MAX_LENGTH = 20;
        }

        public class Business
        {
            public const int NAME_MAX_LENGTH = 50;
            public const int ADDRESS_MAX_LENGTH = 50;
            public const int PHONE_MAX_LENGTH = 20;
            public const int DESCRIPTION_MAX_LENGTH = 1000;
            public const int SUPPORTED_LOCATION_MAX_LENGTH = 200;
        }

        public class Comment
        {
            public const int CONTENT_MAX_LENGTH = 500;
        }

        public class Confirmation
        {
            public const int TYPE_MAX_LENGTH = 20;
        }

        public class OfferedService
        {
            public const decimal DEFAULT_VISITATION_PRICE = 0.0M;
            public const int NAME_MAX_LENGTH = 50;
            public const int DESCRIPTION_MAX_LENGTH = 1000;
        }

        public class ServiceCategory
        {
            public const int NAME_MAX_LENGTH = 50;
        }

        public class VisitationType
        {
            public const int TYPE_MAX_LENGTH = 20;
        }

    }
}
