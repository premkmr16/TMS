using FluentValidation;
using TMS.Application.Common.Helpers;
using TMS.Application.Common.Validators;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Helpers;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Features.Employees.Validators;

/// <summary>
/// Validator for the <see cref="ImportEmployeeRequest"/> class.
/// Ensures that all required fields meet the specified validation rules.
/// </summary>
public class ImportEmployeeRequestValidator : AbstractValidator<ImportEmployeeRequest>
{
    /// <summary>
    /// Initializes the new instance of <see cref="ImportEmployeeRequestValidator"/>
    /// Defines validation rules for import employee Request.
    /// </summary>
    public ImportEmployeeRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(importEmployeeRequest => importEmployeeRequest.Name)
            .Must(employeeName => !string.IsNullOrWhiteSpace(employeeName))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.Name)));

        RuleFor(importEmployeeRequest => importEmployeeRequest.DateOfBirth)
            .Must(dateOfBirth => dateOfBirth.IsValidDate())
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.DateOfBirth)))
            .Must(dateOfBirth => DateTime.Today >= dateOfBirth.AddYears(18))
            .WithMessage(EmployeeValidationMessages.InvalidDateOfBirth);
        
        RuleFor(importEmployeeRequest => importEmployeeRequest.Phone)
            .Must(phone => !string.IsNullOrWhiteSpace(phone))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.Phone)))
            .Length(10)
            .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberLength, nameof(ImportEmployeeRequest.Phone)))
            .Matches(RegexValidation.PhoneNumberRegexPattern)
            .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberFormat, nameof(ImportEmployeeRequest.Phone)));
        
        RuleFor(importEmployeeRequest => importEmployeeRequest.EmployeeNumber)
            .Must(employeeNumber => !string.IsNullOrWhiteSpace(employeeNumber))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.EmployeeNumber)));

        RuleFor(importEmployeeRequest => importEmployeeRequest.Email)
            .Must(emailAddress => !string.IsNullOrWhiteSpace(emailAddress))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.Email)))
            .SetValidator(new EmailValidator());
        
        RuleFor(importEmployeeRequest => importEmployeeRequest.StartDate)
            .Must(startDate => startDate != DateTimeOffset.MinValue && startDate != DateTimeOffset.MaxValue)
            .WithMessage(baseEmployeeRequest => string.Format(ValidationMessages.InvalidDateFormat, nameof(ImportEmployeeRequest.StartDate), baseEmployeeRequest.StartDate));
        
        RuleFor(x => x.EmployeeType)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(ImportEmployeeRequest.EmployeeType)))
            .DependentRules(() =>
            {
                RuleFor(x => x.EmployeeType)
                    .Must((_, employeeType, context) => EmployeeHelper.IsValidEmployeeType("employeeTypes", context, employeeType))
                    .WithMessage(x => string.Format(EmployeeValidationMessages.InvalidEmployeeType, x.EmployeeType));

                RuleFor(x => x)
                    .Must(x => x.EndDate >= x.StartDate.AddMonths(1))
                    .WithMessage(x => string.Format(ValidationMessages.EndDateTooSoon, nameof(ImportEmployeeRequest.EndDate), x.StartDate.AddMonths(1).Date.ToShortDateString()))
                    .When((x, context) => EmployeeHelper.IsValidEmployeeType("specialEmployeeTypes", context, x.EmployeeType));
            });
    }
}