using FluentValidation;
using TMS.Application.Common.Validators;
using TMS.Application.Features.Employees.Contracts.Create;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Features.Employees.Validators;

/// <summary>
/// 
/// </summary>
public class ImportEmployeeRequestListValidator :  AbstractValidator<List<ImportEmployeeRequest>>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="validator"></param>
    public ImportEmployeeRequestListValidator(IValidator<ImportEmployeeRequest> validator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleForEach(x => x).SetValidator(validator);

        RuleFor(employees => employees)
            .SetValidator(new DistinctItemValidator<ImportEmployeeRequest, string>
                (nameof(ImportEmployeeRequest.Email), x => x.Email));
        
        RuleFor(employees => employees)
            .SetValidator(new DistinctItemValidator<ImportEmployeeRequest, string>
                (nameof(ImportEmployeeRequest.EmployeeNumber), x => x.EmployeeNumber));
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class ImportEmployeeRequestValidator :  AbstractValidator<ImportEmployeeRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public ImportEmployeeRequestValidator()
        {
            RuleFor(x => x.Name)
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.Name)));
            
            RuleFor(x => x.Email)
                .Must(emailAddress => !string.IsNullOrWhiteSpace(emailAddress))
                .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.Email)))
                .EmailAddress()
                .WithMessage(x => string.Format(ValidationMessages.InvalidEmail, x.Email));
            
            RuleFor(x => x.Phone)
                .Must(phone => !string.IsNullOrWhiteSpace(phone))
                .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.Phone)))
                .Length(10)
                .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberLength, nameof(ImportEmployeeRequest.Phone)))
                .Matches(RegexValidation.PhoneNumberRegexPattern)
                .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberFormat, nameof(ImportEmployeeRequest.Phone)));
            
            RuleFor(x => x.DateOfBirth)
                .Must(dateOfBirth => dateOfBirth != DateTime.MinValue && dateOfBirth != DateTime.MaxValue)
                .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.DateOfBirth)))
                .Must(dateOfBirth => DateTime.Today >= dateOfBirth.AddYears(18))
                .WithMessage(EmployeeValidationMessages.InvalidDateOfBirth);
            
            RuleFor(x => x.EmployeeType)
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.EmployeeType)));
            
            RuleFor(x => x.EmployeeType)
                .Must((_, employeeType, context) =>
                    context.RootContextData.TryGetValue("employeeTypes", out var employeeTypes) &&
                    employeeTypes is List<string> validEmployeeTypes &&
                    validEmployeeTypes.Contains(employeeType))
                .WithMessage(x => string.Format(EmployeeValidationMessages.InvalidEmployeeType, x.EmployeeType));
            
            RuleFor(x => x.StartDate)
                .Must(startDate => startDate != DateTimeOffset.MinValue)
                .WithMessage(createEmployeeRequest => string.Format(ValidationMessages.InvalidDateFormat, nameof(ImportEmployeeRequest.StartDate), createEmployeeRequest.StartDate));
            
            RuleFor(x => x)
                .Must(x => x.EndDate > x.StartDate.AddMonths(1))
                .WithMessage(x => string.Format(ValidationMessages.EndDateTooSoon, 
                    nameof(ImportEmployeeRequest.EndDate), x.StartDate.AddMonths(1).Date.ToShortDateString()))
                .When((x, context) =>
                    context.RootContextData.TryGetValue("specialEmployeeTypes", out var employeeTypes) &&
                    employeeTypes is List<string> validEmployeeTypes &&
                    validEmployeeTypes.Contains(x.EmployeeType));
        }
    }
}
