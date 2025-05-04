using FluentValidation;
using TMS.Application.Features.Employees.Contracts.Create;

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
    /// Implements the functionality to check the EmployeeType during Employee create or update.
    /// </summary>
    /// <param name="employeeTypeId">The unique Identifier of EmployeeType.</param>
    /// <returns>The boolean value based on EmployeeType</returns>
    public Task<bool> ValidateEmployeeTypeAsync(string employeeTypeId);

    /// <summary>
    /// Implements the functionality to check if the employee type exists.
    /// </summary>
    /// <param name="employeeTypeName">the type of employee</param>
    /// <param name="context">The employee type validation context to capture validation errors.</param>
    /// <returns></returns>
    public Task ValidateEmployeeTypeNameAsync(string employeeTypeName,
        ValidationContext<CreateEmployeeTypeRequest> context);

    /// <summary>
    /// Implements the functionality to check the EndDate for Employee Type.
    /// </summary>
    /// <param name="employeeTypeId">The employee type ID.</param>
    /// <param name="startDate">The start date of the employee.</param>
    /// <param name="endDate">The end date of the employee.</param>
    /// <param name="context">The employee validation context to capture validation errors.</param>
    /// <returns>The <see cref="Task"/></returns>
    public Task ValidateEmployeeEndDate<T>(string employeeTypeId, DateTimeOffset startDate, DateTimeOffset endDate,
        ValidationContext<T> context);
}