namespace TMS.Core.Common.Errors;

public static class ErrorMessages
{
    public static class Database
    {
        public const string ConcurrencyConflict = "A concurrency conflict occurred. The resource was modified by another process. Please refresh and try again.";
        public const string UpdateFailed = "A database error occurred. Please try again later.";
        public const string PostgresError = "A database error occurred. Please try again later.";
        public const string UnexpectedError = "An unexpected error occurred. Please try again later";
    }
    
    public static class ValidationMessages
    {
        public const string CannotBeNullOrEmpty = "{0} is required and cannot be null or empty.";
        public const string InvalidEmail = "{0} must be a valid email address.";
        public const string InvalidUlidFormat = "The ULID format for {0} is invalid: '{1}'.";
        public const string InvalidUlidLength = "{0} must be exactly 26 characters long and contain only valid Base32 characters.";
        public const string InvalidDateFormat = "The date format for {0} is invalid: '{1}'.";
        public const string EndDateTooSoon = "{0} must be at least one month after the Start Date (minimum: {1}).";
        public const string InvalidPhoneNumberLength = "{0} must contain exactly 10 digits";
        public const string InvalidPhoneNumberFormat = "{0} must be exactly 10 digits and contain only numbers";
    }

    public static class RegexValidation
    {
        public const string UlidRegexPattern = "^[0-9ABCDEFGHJKMNPQRSTVWXYZ]{26}$";
        public const string PhoneNumberRegexPattern = @"^\d{10}$";
    }
    
    public static class EmployeeValidationMessages
    {
        public const string EmailInUse = "The email address '{0}' is already assigned to another employee. Please use a different email address or verify the details before proceeding.";
        public const string EmailNotEditable = "The EmailAddress field in employee is not editable.";
        public const string EmployeeNumberInUse = "The Employee Number '{0}' is already assigned to employee '{1}' . Please use a different email address or verify the details before proceeding.";
        public const string EmployeeNotFound = "No employee record found with the provided employee number : {0}";
    }
    
    public static class EmployeeTypeValidationMessages
    {
        public const string EmployeeTypeFound = "employee type record already exists with the provided name : {0}";
    }
}