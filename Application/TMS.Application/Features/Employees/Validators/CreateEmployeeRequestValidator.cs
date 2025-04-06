using FluentValidation;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.ValidationHelpers;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Features.Employees.Validators;

/// <summary>
/// Validator for the <see cref="CreateEmployeeRequest"/> class.
/// Ensures that all required fields meet the specified validation rules.
/// </summary>
public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    #region Validator
    
    /// <summary>
    /// Initializes the new instance of <see cref="CreateEmployeeRequestValidator"/>
    /// Defines validation rules for employee Create Request.
    /// </summary>
    public CreateEmployeeRequestValidator(IEmployeeValidator employeeValidator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(createEmployeeRequest => createEmployeeRequest.EmployeeNumber)
            .Must(employeeNumber => !string.IsNullOrWhiteSpace(employeeNumber))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.EmployeeNumber)));

        RuleFor(createEmployeeRequest => createEmployeeRequest.Name)
            .Must(employeeName => !string.IsNullOrWhiteSpace(employeeName))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Name)));

        RuleFor(createEmployeeRequest => createEmployeeRequest.Email)
            .Must(emailAddress => !string.IsNullOrWhiteSpace(emailAddress))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Email)))
            .EmailAddress()
            .WithMessage(createEmployeeRequest => string.Format(ValidationMessages.InvalidEmail, createEmployeeRequest.Email));

        RuleFor(createEmployeeRequest => createEmployeeRequest.EmployeeTypeId.ToString())
            .Must(employeeTypeId => !string.IsNullOrWhiteSpace(employeeTypeId))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.EmployeeTypeId)))
            .Length(26)
            .WithMessage(string.Format(ValidationMessages.InvalidUlidLength, nameof(CreateEmployeeRequest.EmployeeTypeId)))
            .Matches(RegexValidation.UlidRegexPattern)
            .WithMessage(createEmployeeRequest => string.Format(ValidationMessages.InvalidUlidFormat, nameof(CreateEmployeeRequest.EmployeeTypeId), createEmployeeRequest.EmployeeTypeId));

        RuleFor(createEmployeeRequest => createEmployeeRequest.StartDate)
            .Must(startDate => startDate != DateTimeOffset.MinValue)
            .WithMessage(createEmployeeRequest => string.Format(ValidationMessages.InvalidDateFormat, nameof(CreateEmployeeRequest.StartDate), createEmployeeRequest.StartDate));

        RuleFor(createEmployeeRequest => createEmployeeRequest.EndDate)
            .GreaterThanOrEqualTo(createEmployeeRequest => createEmployeeRequest.StartDate.AddMonths(1))
            .WithMessage(createEmployeeRequest => string.Format(ValidationMessages.EndDateTooSoon, nameof(CreateEmployeeRequest.EndDate), createEmployeeRequest.EndDate))
            .WhenAsync(async (createEmployeeRequest, cancellationToken) => 
                !createEmployeeRequest.IsVerified && await employeeValidator.ValidateEmployeeTypeAsync(createEmployeeRequest.EmployeeTypeId));
        
        RuleFor(createEmployeeRequest => createEmployeeRequest.Phone)
            .Must(phone => !string.IsNullOrWhiteSpace(phone))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Phone)))
            .Length(10)
            .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberLength, nameof(CreateEmployeeRequest.Phone)))
            .Matches(RegexValidation.PhoneNumberRegexPattern)
            .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberFormat, nameof(CreateEmployeeRequest.Phone)));

        RuleFor(createEmployeeRequest => createEmployeeRequest)
            .CustomAsync(async (createEmployeeRequest, context, cancellationToken) => 
                await employeeValidator.ValidateCreateEmployeeRequestAsync(createEmployeeRequest.EmployeeNumber, createEmployeeRequest.Email, context))
            .When(createEmployeeRequest => 
                !createEmployeeRequest.IsVerified && 
                !string.IsNullOrWhiteSpace(createEmployeeRequest.Email) && 
                !string.IsNullOrWhiteSpace(createEmployeeRequest.EmployeeNumber));
    }
    
    #endregion
}