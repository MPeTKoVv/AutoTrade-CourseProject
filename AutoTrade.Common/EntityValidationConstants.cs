namespace AutoTrade.Common
{
    public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class Condition
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }


        public static class Image
        {
            public const int UrlMaxLength = 2048;
        }

        public static class Car
        {
            public const int MakeMinLength = 2;
            public const int MakeMaxLength = 50;

            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 50;

            public const int CountryMinLength = 2;
            public const int CountryMaxLength = 100;

            public const int HorsepowerMinValue = 1;
            public const int HorsepowerMaxValue = 1500;

            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 500;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "99999999";

            public const int ColorMinLength = 2;
            public const int ColorMaxLength = 20;

            public const int UrlMaxLength = 2048;
        }

        public static class Seller
        {
            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }
    }
}
