﻿namespace AutoTrade.Common
{
    public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class Engine
        {
            public const int TypeMinLength = 2;
            public const int TypeMaxLength = 50;
        }

        public static class Car
        {
            public const int MakeMinLength = 2;
            public const int MakeMaxLength = 50;

            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 50;

            public const int CountryMinLength = 2;
            public const int CountryMaxLength = 100;

            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 500;

			public const string PriceMinValue = "1";
			public const string PriceMaxValue = "999999999";

			public const string HorsepowerMinValue = "1";
			public const string HorsepowerMaxValue = "2000";

			public const string YearMinValue = "1886";
			public const string YearMaxValue = "2023";

			public const string MileageMinValue = "0";
			public const string MileageMaxValue = "9999999";

            public const int ImageUrlMinLength = 5;
            public const int ImageUrlMaxLength = 2048;
		}

        public static class Seller
        {
            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }

        public static class Transmission
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class User
        {
            public const int PasswordMinLenght = 6;
            public const int PasswordMaxLenght = 100;

            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 15;

			public const int LastNameMinLength = 1;
			public const int LastNameMaxLength = 15;
		}

        public static class CreditCard
        {
            public const int NameOnCardMinLength = 2;
            public const int NameOnCardMaxLength = 30;

            public const int CreditNumberLength = 16;

            public const int BillingAddressMinLength = 5;
            public const int BillingAddressMaxLength = 100;

            public const int CountryMinLength = 2;
            public const int CountryMaxLength = 60;

            public const int CVVCodeMaxLength = 4;

            public const int ExpirationDateMaxLength = 5;

            public const string WithdrawMinValue = "1";
			public const string WithdrawMaxValue = "1000000";
		}
    }
}
