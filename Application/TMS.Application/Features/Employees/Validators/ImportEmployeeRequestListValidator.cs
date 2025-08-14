using FluentValidation;
using TMS.Application.Common.Validators;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Helpers;
using TMS.Application.Features.Employees.ValidationHelpers;

namespace TMS.Application.Features.Employees.Validators;

/// <summary>
/// Validator for the <see cref="ImportEmployee"/> class.
/// Ensures that all required fields meet the specified validation rules.
/// </summary>
public class ImportEmployeeRequestListValidator : AbstractValidator<ImportEmployee>
{
    /// <summary>
    /// Initializes the new instance of <see cref="ImportEmployeeRequestListValidator"/>
    /// Defines validation rules for import employee Request.
    /// </summary>
    /// <param name="validator">Defines the fluent validator for <see cref="IValidator{ImportEmployeeRequest}"/>.</param>
    /// <param name="employeeValidator">Defines the custom validator <see cref="IEmployeeValidator"/></param>
    public ImportEmployeeRequestListValidator(
        IValidator<ImportEmployeeRequest> validator,
        IEmployeeValidator employeeValidator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleForEach(x => x.ImportEmployeeRequests)
            .SetValidator(validator);

        RuleFor(employee => employee)
            .ChildRules(x =>
            {
                x.RuleFor(employee => employee.ImportEmployeeRequests)
                    .SetValidator(new DistinctItemValidator<ImportEmployeeRequest, string>(
                        nameof(ImportEmployeeRequest.Email), employee => employee.Email));
                
                x.RuleFor(employee => employee.ImportEmployeeRequests)
                    .SetValidator(new DistinctItemValidator<ImportEmployeeRequest, string>(
                        nameof(ImportEmployeeRequest.EmployeeNumber), employee => employee.EmployeeNumber));
            })
            .DependentRules(() =>
            {
                RuleFor(employee => employee.ImportEmployeeRequests)
                    .CustomAsync(async (employeeRequest, context, cancellationToken) =>
                    {
                        var employeeIdentifier = employeeRequest.GetEmployeeNumberAndEmail();
                        
                        await employeeValidator.ValidateExcelEmployeeData(
                            employeeIdentifier.employeeNumbers,
                            employeeIdentifier.emails,
                            context);
                    });
            });
    }
}