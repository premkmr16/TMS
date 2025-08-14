using System.Text.RegularExpressions;
using FluentValidation;
using TMS.Application.Common.Validators;
using TMS.Application.Features.Employees.Contracts.Update;
using TMS.Application.Features.Employees.ValidationHelpers;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Features.Employees.Validators;

/// <summary>
/// Validator for the <see cref="UpdateEmployeeRequest"/> class.
/// Ensures that all required fields meet the specified validation rules.
/// </summary>
public class UpdateEmployeeRequestValidator  : BaseEmployeeContractValidator<UpdateEmployeeRequest>
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
            .Must(id => !string.IsNullOrWhiteSpace(id))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(UpdateEmployeeRequest.Id)))
            .SetValidator(new UlidValidator());
        
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