using System.Text.RegularExpressions;
using FluentValidation;
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
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest.Id)
            .Must(employeeId => !string.IsNullOrWhiteSpace(employeeId))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(UpdateEmployeeRequest.Id)))
            .Length(26)
            .WithMessage(string.Format(ValidationMessages.InvalidStringLength, nameof(UpdateEmployeeRequest.Id)))
            .Matches(RegexValidation.StringRegexPattern)
            .WithMessage(updateEmployeeRequest => string.Format(ValidationMessages.InvalidStringFormat, nameof(UpdateEmployeeRequest.Id), updateEmployeeRequest.Id));
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest.Name)
            .Must(employeeName => !string.IsNullOrWhiteSpace(employeeName))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(UpdateEmployeeRequest.Name)));
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest.DateOfBirth)
            .Must(dateOfBirth => dateOfBirth != DateTime.MinValue && dateOfBirth != DateTime.MaxValue)
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(UpdateEmployeeRequest.DateOfBirth)))
            .Must(dateOfBirth => DateTime.Today >= dateOfBirth.AddYears(18))
            .WithMessage(EmployeeValidationMessages.InvalidDateOfBirth);
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest.EmployeeTypeId)
            .Must(employeeTypeId => !string.IsNullOrWhiteSpace(employeeTypeId))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(UpdateEmployeeRequest.EmployeeTypeId)))
            .Length(26)
            .WithMessage(string.Format(ValidationMessages.InvalidStringLength, nameof(UpdateEmployeeRequest.EmployeeTypeId)))
            .Matches(RegexValidation.StringRegexPattern)
            .WithMessage(updateEmployeeRequest => string.Format(ValidationMessages.InvalidStringFormat, nameof(UpdateEmployeeRequest.EmployeeTypeId), updateEmployeeRequest.EmployeeTypeId));
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest.StartDate)
            .Must(startDate => startDate != DateTimeOffset.MinValue)
            .WithMessage(updateEmployeeRequest => string.Format(ValidationMessages.InvalidDateFormat, nameof(UpdateEmployeeRequest.StartDate), updateEmployeeRequest.StartDate));
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest.Phone)
            .Must(phone => !string.IsNullOrWhiteSpace(phone))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(UpdateEmployeeRequest.Phone)))
            .Length(10)
            .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberLength, nameof(UpdateEmployeeRequest.Phone)))
            .Matches(RegexValidation.PhoneNumberRegexPattern)
            .WithMessage(string.Format(ValidationMessages.InvalidPhoneNumberFormat, nameof(UpdateEmployeeRequest.Phone)));
        
        RuleFor(updateEmployeeRequest => updateEmployeeRequest)
            .CustomAsync(async (createEmployeeRequest, context, cancellationToken) => 
                await employeeValidator.ValidateEmployeeEndDate(
                    createEmployeeRequest.EmployeeTypeId, createEmployeeRequest.StartDate, createEmployeeRequest.EndDate, context))
            .When(createEmployeeRequest =>
                Regex.IsMatch(createEmployeeRequest.EmployeeTypeId, RegexValidation.StringRegexPattern)
            );
    }
    
    #endregion
}