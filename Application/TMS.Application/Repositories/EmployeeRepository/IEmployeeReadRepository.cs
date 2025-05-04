using TMS.Application.Common.Models;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Core.Entities;

namespace TMS.Application.Repositories.EmployeeRepository;

/// <summary>
/// Defines the method for reading employee related data from database
/// </summary>
public interface IEmployeeReadRepository
{
    /// <summary>
    /// Implements the functionality to return the Employee data for the given EmployeeID.
    /// </summary>
    /// <param name="employeeId">Defines the unique Id of employee</param>
    /// <returns>The employee <see cref="EmployeeResponse"/></returns>
    public Task<EmployeeResponse> GetEmployee(string employeeId);

    /// <summary>
    /// Implements the functionality to return the Employee data for the given EmployeeID.
    /// </summary>
    /// <param name="employeeId">Defines the unique Id of employee</param>
    /// <returns>The employee <see cref="Employee"/></returns>
    public Task<Employee> GetEmployeeWithoutTypeName(string employeeId);
    
    /// <summary>
    /// Implements the functionality to return all Employee data.
    /// </summary>
    /// <returns>The list of employees <see cref="List{EmployeeResponse}"/></returns>
    public Task<PaginatedResponse<EmployeeResponse>> GetEmployees(PaginationRequest request);
    
    /// <summary>
    /// Implements the functionality to return the Employee data for the given Employee EmailAddress.
    /// </summary>
    /// <param name="employeeNumber">Defines the unique Number of employee</param>
    /// <param name="emailAddress">Defines the unique EmailAddress of employee</param>
    /// <returns>The employee <see cref="EmployeeResponse"/></returns>
    public Task<List<EmployeeResponse>> GetEmployeeByNumberOrEmail(string employeeNumber = null, string emailAddress = null);
    
    /// <summary>
    /// Implements the functionality to return the Employee Type for the given EmployeeTypeId.
    /// </summary>
    /// <param name="employeeTypeId">Defines the unique Id of employee </param>
    /// <returns>The employee type <see cref="EmployeeType"/>.</returns>
    public Task<EmployeeType> GetEmployeeType(string employeeTypeId);
    
    /// <summary>
    /// Implements the functionality to return all Employee Types.
    /// </summary>
    /// <returns>The list of employee types <see cref="List{EmployeeType}"/>.</returns>
    public Task<List<EmployeeType>> GetEmployeeTypes();

    /// <summary>
    /// Implements the functionality to return employee type by name.
    /// </summary>
    /// <param name="employeeTypeName">Defines the type of employee</param>
    /// <returns>The employee type <see cref="EmployeeType"/>.</returns>
    public Task<EmployeeType> GetEmployeeTypeByName(string employeeTypeName);
}