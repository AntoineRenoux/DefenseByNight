namespace DefenseByNight.API.Helpers
{
    public static class ValidationsRegex
    {
        public const string PhoneNumberRegex = "(0|\\+33)[1-9](( |\\.)*[0-9]{2}){4}";
        public const string FirstNameRegex = "^[a-zA-Z](( |-)?[a-zA-Zà-ü]*){2,20}";
        public const string LastNameRegex = "^[a-zA-Z](( |-)?[a-zA-Zà-ü]*){2,20}";
        public const string UserNameRegex = "(( |-|_)?[a-zA-Z0-9à-ü]*){2,20}";
        public const string AddressRegex = "[a-zA-Z0-9à-ü ]{0,100}";
        public const string CityRegex = "^[a-zA-Z](( |-)?[a-zA-Z]*)+";
        public const string ZipCodeRegex = "[0-9]{5}";
        public const string EmailRegex = "([a-z0-9])*((\\.|_)*[a-z0-9])+@[a-z]+\\.[a-z]{2,3}";
    }
}