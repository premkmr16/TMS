using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Repositories.EmployeeRepository;
using TMS.Core.Entities;
using TMS.Infrastructure.Persistence.Context;

namespace TMS.Infrastructure.Persistence.Repositories.EmployeeRepository;

/// <summary>
/// Implements methods for writing Employee Related data to the database.
/// </summary>
public class EmployeeWriteRepository : IEmployeeWriteRepository
{
    /// <summary>
    /// The name of the repository used for logging.
    /// </summary>
    private const string RepositoryName = nameof(EmployeeWriteRepository);

    /// <summary>
    /// Database context for performing Employee related operations.
    /// </summary>
    private readonly TmsDbContext _context;

    /// <summary>
    /// Logger instance for capturing repository logs.
    /// </summary>
    private readonly ILogger<EmployeeWriteRepository> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeWriteRepository"/> class.
    /// </summary>
    /// <param name="context">Defines the database context <see cref="TmsDbContext"/>.</param>
    /// <param name="logger">Defines the logger instance <see cref="ILogger{EmployeeWriteRepository}"/>.</param>
    public EmployeeWriteRepository(
        TmsDbContext context,
        ILogger<EmployeeWriteRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc cref="IEmployeeWriteRepository.AddEmployee"/>
    public async Task<Employee> AddEmployee(Employee employee)
    {
        const string methodName = nameof(AddEmployee);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully with input : {@Employee}",
            RepositoryName, methodName, employee);

        var addedEmployee = await _context.Employees.AddAsync(employee);
        var rowsAffected = await _context.SaveChangesAsync();

        if (rowsAffected <= 0)
        {
            _logger.LogError(
                "{Repository}.{Method} - Changes were not saved to the database for entity: {@Entities}",
                RepositoryName, methodName, addedEmployee.Entity);

            var errorMessage = $"Failed to save changes for Employee: {@employee} to the database.";
            throw new DbUpdateException(errorMessage);
        }

        _logger.LogInformation(
            "{Repository}.{Method} - Execution completed successfully with {rowsAffected} entity added to the database: {@AddedEmployee}",
            RepositoryName, methodName, rowsAffected, addedEmployee.Entity);

        return addedEmployee.Entity;
    }

    /// <inheritdoc cref="IEmployeeWriteRepository.UpdateEmployee"/>
    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        const string methodName = nameof(AddEmployee);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully with input : {@Employee}",
            RepositoryName, methodName, employee);

        var updatedEmployee = _context.Employees.Update(employee);
        var rowsAffected = await _context.SaveChangesAsync();

        if (rowsAffected <= 0)
        {
            _logger.LogError(
                "{Repository}.{Method} - Changes were not saved to the database for entity: {@Entities}",
                RepositoryName, methodName, updatedEmployee.Entity);

            var errorMessage = $"Failed to save changes for Employee: {@employee} to the database.";
            throw new DbUpdateException(errorMessage);
        }

        _logger.LogInformation(
            "{Repository}.{Method} - Execution completed successfully with {rowsAffected} entity updated to the database: {@UpdatedEmployee}",
            RepositoryName, methodName, rowsAffected, updatedEmployee.Entity);

        return updatedEmployee.Entity;
    }

    /// <inheritdoc cref="IEmployeeWriteRepository.AddEmployees"/>
    public async Task AddEmployees(List<Employee> employees)
    {
        const string methodName = nameof(AddEmployee);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully with input : {@Employees}",
            RepositoryName, methodName, employees);

        await _context.Employees.AddRangeAsync(employees);
        var rowsAffected = await _context.SaveChangesAsync();

        if (rowsAffected <= 0)
        {
            _logger.LogError(
                "{Repository}.{Method} - Changes were not saved to the database for entity: {@Entities}",
                RepositoryName, methodName, employees);

            var errorMessage = $"Failed to save changes for Employees: {@employees} to the database.";
            throw new DbUpdateException(errorMessage);
        }

        _logger.LogInformation(
            "{Repository}.{Method} - Execution completed successfully with {rowsAffected} entity added to the database: {@AddedEmployees}",
            RepositoryName, methodName, rowsAffected, employees);
    }

    /// <inheritdoc cref="IEmployeeWriteRepository.AddEmployeeType"/>
    public async Task<EmployeeType> AddEmployeeType(EmployeeType employeeType)
    {
        const string methodName = nameof(AddEmployeeType);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully with input : {@EmployeeType}",
            RepositoryName, methodName, employeeType);

        var addedEmployeeType = await _context.EmployeeTypes.AddAsync(employeeType);
        var rowsAffected = await _context.SaveChangesAsync();

        if (rowsAffected <= 0)
        {
            _logger.LogError(
                "{Repository}.{Method} - Changes were not saved to the database for entity: {@Entities}",
                RepositoryName, methodName, addedEmployeeType.Entity);

            var errorMessage = $"Failed to save changes for EmployeeType: {@addedEmployeeType} to the database.";
            throw new DbUpdateException(errorMessage);
        }
        
        _logger.LogInformation(
            "{Repository}.{Method} - Execution completed successfully with {rowsAffected} entity added to the database: {@AddedEmployeeType}",
            RepositoryName, methodName, rowsAffected, addedEmployeeType.Entity);
        
        return addedEmployeeType.Entity;
    }
}