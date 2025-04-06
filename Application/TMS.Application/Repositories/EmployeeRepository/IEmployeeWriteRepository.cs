using TMS.Core.Entities;

namespace TMS.Application.Repositories.EmployeeRepository;

/// <summary>
/// Defines methods for writing Employee Related data to the database.
/// </summary>
public interface IEmployeeWriteRepository
{
    /// <summary>
    /// Implements the functionality to add the Employee data to database.
    /// </summary>
    /// <param name="employee">Defines the employee entity data to be added <see cref="Employee"/>.</param>
    /// <returns>The added employee entity <see cref="Employee"/>.</returns>
    public Task<Employee> AddEmployee(Employee employee);
    
    /// <summary>
    /// Implements the functionality to update the Employee data to database.
    /// </summary>
    /// <param name="employee">Defines the employee entity data to be updated <see cref="Employee"/></param>
    /// <returns>The updated employee entity <see cref="Employee"/>.</returns>
    public Task<Employee> UpdateEmployee(Employee employee);
    
    /// <summary>
    /// Implements the functionality to add bulk Employee data to database.
    /// </summary>
    /// <param name="employees">Defines the bulk Employee entity to be added <see cref="List{T}"/></param>
    /// <returns></returns>
    public Task AddEmployees(List<Employee> employees);
    
    /// <summary>
    /// Implements the functionality to add the EmployeeType data to database.
    /// </summary>
    /// <param name="employeeType">Defines the EmployeeType entity to be added <see cref="EmployeeType"/>.</param>
    /// <returns></returns>
    public Task<EmployeeType> AddEmployeeType(EmployeeType employeeType);
}