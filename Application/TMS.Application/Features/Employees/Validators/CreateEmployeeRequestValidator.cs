using System.Text.RegularExpressions;
using FluentValidation;
using TMS.Application.Common.Validators;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.ValidationHelpers;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Features.Employees.Validators;

/// <summary>
/// Validator for the <see cref="CreateEmployeeRequest"/> class.
/// Ensures that all required fields meet the specified validation rules.
/// </summary>
public class CreateEmployeeRequestValidator : BaseEmployeeContractValidator<CreateEmployeeRequest>
{
    #region Validator
    
    /// <summary>
    /// Initializes the new instance of <see cref="CreateEmployeeRequestValidator"/>
    /// Defines validation rules for employee Create Request.
    /// </summary>
    /// <param name="employeeValidator">Defines the custom validator <see cref="IEmployeeValidator"/>.</param>
    public CreateEmployeeRequestValidator(IEmployeeValidator employeeValidator)
    {
         RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(createEmployeeRequest => createEmployeeRequest.EmployeeNumber)
            .Must(employeeNumber => !string.IsNullOrWhiteSpace(employeeNumber))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.EmployeeNumber)));

        RuleFor(createEmployeeRequest => createEmployeeRequest.Email)
            .Must(emailAddress => !string.IsNullOrWhiteSpace(emailAddress))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeRequest.Email)))
            .SetValidator(new EmailValidator());

        RuleFor(createEmployeeRequest => createEmployeeRequest)
            .CustomAsync(async (createEmployeeRequest, context, cancellationToken) => 
                await employeeValidator.ValidateCreateEmployeeRequestAsync(createEmployeeRequest.EmployeeNumber, createEmployeeRequest.Email, context))
            .When(createEmployeeRequest =>
                !createEmployeeRequest.IsVerified &&
                !string.IsNullOrWhiteSpace(createEmployeeRequest.Email) &&
                !string.IsNullOrWhiteSpace(createEmployeeRequest.EmployeeNumber)
            );
        
        RuleFor(createEmployeeRequest => createEmployeeRequest)
            .CustomAsync(async (createEmployeeRequest, context, cancellationToken) => 
                await employeeValidator.ValidateEmployeeEndDate(
                    createEmployeeRequest.EmployeeTypeId, createEmployeeRequest.StartDate, createEmployeeRequest.EndDate, context))
            .When(createEmployeeRequest =>
                createEmployeeRequest.IsVerified == false &&
                Regex.IsMatch(createEmployeeRequest.EmployeeTypeId, RegexValidation.StringRegexPattern)
            );
    }
    
    #endregion
}