namespace MVVMPrivateClinicProjectDesktopApp.Helpers;

public static class RegexPatterns {
    public const string LettersOnlyRegex = @"([\p{L}]+[\s]?)+";
    public const string LettersOnlyRegexWithAdditionalCharacters = @"([\p{L} .,!?]+[\s]?)+";
    public const string PhoneNumberRegex = @"(((\d{3}-?){3})?((\d{3}\s?){3})?(\d{9})?)?";
    public const string EmailAddressRegex = @"([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})";
    public const string PostalCodeRegex = @"\d{2}-\d{3}";
    public const string BuildingNumberRegex = @"\d+([\p{L}]+[\s]?)*";
}