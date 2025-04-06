using FluentValidation;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Contracts.Update;

namespace TMS.Application.Features.Employees.ValidationHelpers;

/// <summary>
/// Defines the methods to Validate the Employee Related Information.
/// </summary>
public interface IEmployeeValidator
{
    /// <summary>
    /// Implements the functionality to check for duplicate Employee Number or Email added during Employee creation.
    /// </summary>
    /// <param name="newEmployeeNumber">The unique Number assigned to employee during creation.</param>
    /// <param name="newEmailAddress">The unique Email assigned to employee during creation.</param>
    /// <param name="context">The employee validation context to capture validation errors.</param>
    /// <returns>The <see cref="Task"/></returns>
    public Task ValidateCreateEmployeeRequestAsync(string newEmployeeNumber, string newEmailAddress,
        ValidationContext<CreateEmployeeRequest> context);

    /// <summary>
    /// Implements the functionality to prevent the Employee EmailAddress changed during Employee update.
    /// </summary>
    /// <param name="employeeNumber">The unique Number assigned to employee.</param>
    /// <param name="emailAddress">The unique Email assigned to employee.</param>
    /// <param name="context">The employee validation context to capture validation errors.</param>
    /// <returns>The <see cref="Task"/></returns>
    public Task ValidateUpdateEmployeeRequestAsync(string employeeNumber, string emailAddress,
        ValidationContext<UpdateEmployeeRequest> context);

    /// <summary>
    /// Implements the functionality to check the EmployeeType during Employee create or update.
    /// </summary>
    /// <param name="employeeTypeId">The unique Identifier of EmployeeType.</param>
    /// <returns>The boolean value based on EmployeeType</returns>
    public Task<bool> ValidateEmployeeTypeAsync(Ulid employeeTypeId);

    /// <summary>
    /// Implements the functionality to check if the employee type exists.
    /// </summary>
    /// <param name="employeeTypeName">the type of employee</param>
    /// <param name="context">The employee type validation context to capture validation errors.</param>
    /// <returns></returns>
    public Task ValidateEmployeeTypeNameAsync(string employeeTypeName,
        ValidationContext<CreateEmployeeTypeRequest> context);
}