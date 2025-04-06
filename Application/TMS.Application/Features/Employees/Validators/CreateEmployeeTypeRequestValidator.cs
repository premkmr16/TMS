using FluentValidation;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.ValidationHelpers;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Features.Employees.Validators;

/// <summary>
/// Validator for the <see cref="CreateEmployeeTypeRequest"/> class.
/// Ensures that all required fields meet the specified validation rules.
/// </summary>
public class CreateEmployeeTypeRequestValidator  : AbstractValidator<CreateEmployeeTypeRequest>
{
    /// <summary>
    /// Initializes the new instance of <see cref="CreateEmployeeTypeRequestValidator"/>
    /// Defines validation rules for employee type Create Request.
    /// </summary>
    public CreateEmployeeTypeRequestValidator(IEmployeeValidator employeeValidator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(createEmployeeTypeRequest => createEmployeeTypeRequest.Type)
            .Must(employeeType => !string.IsNullOrWhiteSpace(employeeType))
            .WithMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeTypeRequest.Type)));

        RuleFor(createEmployeeTypeRequest => createEmployeeTypeRequest)
            .CustomAsync(async (createEmployeeTypeRequest, context, cancellationToken) =>
                await employeeValidator.ValidateEmployeeTypeNameAsync(createEmployeeTypeRequest.Type, context))
            .When(createEmployeeTypeRequest => !string.IsNullOrWhiteSpace(createEmployeeTypeRequest.Type));
    }
}