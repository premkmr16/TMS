using FluentValidation;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Contracts.Update;
using TMS.Application.Features.Employees.ValidationHelpers;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Features.Employees.Validators;

/// <summary>
/// Validator for the <see cref="UpdateEmployeeRequest"/> class.
/// Ensures that all required fields meet the specified validation rules.
/// </summary>
public class UpdateEmployeeRequestValidator  : AbstractValidator<UpdateEmployeeRequest>
{
    #region Validator
    
    /// <summary>
    /// Initializes the new instance of <see cref="UpdateEmployeeRequestValidator"/>
    /// Defines validation rules for employee Update Request.
    /// </summary>
    public UpdateEmployeeRequestValidator(IEmployeeValidator employeeValidator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest.EmployeeNumber)
            .Must(employeeNumber => !string.IsNullOrWhiteSpace(employeeNumber))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.EmployeeNumber)));
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest.Name)
            .Must(employeeName => !string.IsNullOrWhiteSpace(employeeName))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Name)));
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest.EmployeeTypeId.ToString())
            .Must(employeeTypeId => !string.IsNullOrWhiteSpace(employeeTypeId))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.EmployeeTypeId)))
            .Length(26)
            .WithMessage(string.Format(ValidationMessages.InvalidUlidLength, nameof(CreateEmployeeRequest.EmployeeTypeId)))
            .Matches(RegexValidation.UlidRegexPattern)
            .WithMessage(updateEmployeeRequest => string.Format(ValidationMessages.InvalidUlidFormat, nameof(CreateEmployeeRequest.EmployeeTypeId), updateEmployeeRequest.EmployeeTypeId));
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest.StartDate)
            .Must(startDate => startDate != DateTimeOffset.MinValue)
            .WithMessage(updateEmployeeRequest => string.Format(ValidationMessages.InvalidDateFormat, nameof(CreateEmployeeRequest.StartDate), updateEmployeeRequest.StartDate));

        RuleFor(createEmployeeRequest => createEmployeeRequest.EndDate)
            .GreaterThanOrEqualTo(createEmployeeRequest => createEmployeeRequest.StartDate.AddMonths(1))
            .WithMessage(createEmployeeRequest => string.Format(ValidationMessages.EndDateTooSoon, nameof(CreateEmployeeRequest.EndDate), createEmployeeRequest.EndDate))
            .WhenAsync(async (createEmployeeRequest, cancellationToken) => await employeeValidator.ValidateEmployeeTypeAsync(createEmployeeRequest.EmployeeTypeId));

        RuleFor(updateEmployeeRequest => updateEmployeeRequest.Phone)
            .Must(phone => !string.IsNullOrWhiteSpace(phone))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Phone)))
            .Length(10)
            .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberLength, nameof(CreateEmployeeRequest.Phone)))
            .Matches(RegexValidation.PhoneNumberRegexPattern)
            .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberFormat, nameof(CreateEmployeeRequest.Phone)));

        RuleFor(updateEmployeeRequest => updateEmployeeRequest)
            .CustomAsync(async (updateEmployeeRequest, context, cancellationToken) =>
                await employeeValidator.ValidateUpdateEmployeeRequestAsync(updateEmployeeRequest.EmployeeNumber, updateEmployeeRequest.Email, context))
            .When(updateEmployeeRequest => !string.IsNullOrWhiteSpace(updateEmployeeRequest.EmployeeNumber));
    }
    
    #endregion
}