using System.Data;
using System.Text.Json;
using Dapper;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Models;
using TMS.Application.ConnectionFactory;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Repositories.EmployeeRepository;
using TMS.Core.Database;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.Repositories.EmployeeRepository;

/// <summary>
/// Implements methods for reading Employee Related data from the database.
/// </summary>
public class EmployeeReadRepository : IEmployeeReadRepository
{
    #region Fields
    
    /// <summary>
    /// The name of the repository used for logging.
    /// </summary>
    private const string RepositoryName = nameof(EmployeeReadRepository);

    /// <summary>
    /// Database Connection for performing Employee related operations.
    /// </summary>
    private readonly IConnectionFactory _connectionFactory;

    /// <summary>
    /// Logger instance for capturing repository logs.
    /// </summary>
    private readonly ILogger<EmployeeReadRepository> _logger;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeReadRepository"/> class.
    /// </summary>
    /// <param name="connectionFactory">Defines the database connection factory <see cref="IConnectionFactory"/></param>
    /// <param name="logger">Defines the logger instance <see cref="ILogger{EmployeeReadRepository}"/></param>
    public EmployeeReadRepository(
        IConnectionFactory connectionFactory,
        ILogger<EmployeeReadRepository> logger)
    {
        _logger = logger;
        _connectionFactory = connectionFactory;
    }
    
    #endregion
    
    #region Employee Read Methods

    /// <inheritdoc cref="IEmployeeReadRepository.GetEmployee"/>
    public async Task<EmployeeResponse> GetEmployee(string employeeId)
    {
        const string methodName = nameof(GetEmployee);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully with input : {EmployeeId}",
            RepositoryName, methodName, employeeId);

        var connection = _connectionFactory.CreateConnection();

        _logger.LogDebug("{Repository}.{Method} - Established connection to Database Successfully", RepositoryName,
            methodName);

        var employee =
            await connection.QueryFirstOrDefaultAsync<EmployeeResponse>(
                sql: EmployeeQueries.GetEmployeeByIdWithEmployeeTypeNameQuery,
                param: new { EmployeeId = employeeId },
                commandType: CommandType.Text,
                commandTimeout: 10);

        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@Employee}",
            RepositoryName, methodName, employee);

        return employee;
    }
    
    /// <inheritdoc cref="IEmployeeReadRepository.GetEmployee"/>
    public async Task<Employee> GetEmployeeWithoutTypeName(string employeeId)
    {
        const string methodName = nameof(GetEmployeeWithoutTypeName);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully with input : {EmployeeId}",
            RepositoryName, methodName, employeeId);

        var connection = _connectionFactory.CreateConnection();

        _logger.LogDebug("{Repository}.{Method} - Established connection to Database Successfully", RepositoryName,
            methodName);

        var employee =
            await connection.QueryFirstOrDefaultAsync<Employee>(
                sql: EmployeeQueries.GetEmployeeByIdWithoutEmployeeTypeNameQuery,
                param: new { EmployeeId = employeeId },
                commandType: CommandType.Text,
                commandTimeout: 10);

        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@Employee}",
            RepositoryName, methodName, employee);

        return employee;
    }

    /// <inheritdoc cref="IEmployeeReadRepository.GetEmployees"/>
    public async Task<PaginatedResponse<EmployeeResponse>> GetEmployees(PaginationRequest request)
    {
        const string methodName = nameof(GetEmployees);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully", RepositoryName, methodName);

        var connection = _connectionFactory.CreateConnection();

        _logger.LogDebug("{Repository}.{Method} - Established connection to Database Successfully", RepositoryName,
            methodName);

        var employees = 
            await connection.QueryFirstOrDefaultAsync<string>(
                sql: EmployeeQueries.GetEmployeesQuery,
                param: new
                {
                    request.SortField, 
                    request.SortDirection, 
                    request.PageSize, 
                    request.PageNumber,
                    Filters = request.Filters is { Count: > 0 } 
                        ? JsonSerializer.Serialize(request.Filters) 
                        : "{}",
                    request.FetchWithPagination
                },
                commandType: CommandType.Text,
                commandTimeout: 10
            );

        var employeeResponse = JsonSerializer.Deserialize<PaginatedResponse<EmployeeResponse>>(employees);

        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@Employees}",
            RepositoryName, methodName, employeeResponse);

        return employeeResponse;
    }

    /// <inheritdoc cref="IEmployeeReadRepository.GetEmployeeByNumberOrEmail"/>
    public async Task<List<EmployeeResponse>> GetEmployeeByNumberOrEmail(string employeeNumber = null,
        string emailAddress = null)
    {
        const string methodName = nameof(GetEmployeeByNumberOrEmail);

        _logger.LogInformation(
            "{Repository}.{Method} - Execution started successfully with input : {EmployeeId} and {EmailAddress}",
            RepositoryName, methodName, employeeNumber, emailAddress);

        var connection = _connectionFactory.CreateConnection();

        _logger.LogDebug("{Repository}.{Method} - Established connection to Database Successfully", RepositoryName,
            methodName);

        var employee =
            (await connection.QueryAsync<EmployeeResponse>(
                sql: EmployeeQueries.GetEmployeeByEmailOrEmployeeNumberQuery,
                param: new { EmailAddress = emailAddress, EmployeeNumber = employeeNumber },
                commandType: CommandType.Text,
                commandTimeout: 10)).ToList();
        
        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@Employee}",
            RepositoryName, methodName, employee);

        return employee;
    }
    
    /// <inheritdoc cref="IEmployeeReadRepository.GetEmployeesByEmailOrNumbers"/>
    public async Task<List<EmployeeResponse>> GetEmployeesByEmailOrNumbers(string employeeNumbers = null, string emailAddresses = null)
    {
        const string methodName = nameof(GetEmployeesByEmailOrNumbers);
        
        _logger.LogInformation("[{Repository}].[{Method}] - Execution started successfully", RepositoryName,
            methodName);
        
        var connection = _connectionFactory.CreateConnection();
        
        _logger.LogDebug("[{Repository}].[{Method}] - Established connection to Database Successfully", RepositoryName,
            methodName);
        
        var employees = (await connection.QueryAsync<EmployeeResponse>(
            sql: EmployeeQueries.GetUniqueEmployeeIdentifiers,
            param: new { EmployeeNumbers = string.Join(", ", employeeNumbers), Emails = emailAddresses },
            commandType: CommandType.Text,
            commandTimeout: 10)).ToList();
        
        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@Employees}",
            RepositoryName, methodName, employees);

        return employees;
    }
    
    #endregion
    
    #region EmployeeType Read Methods

    /// <inheritdoc cref="IEmployeeReadRepository.GetEmployeeType"/>
    public async Task<EmployeeType> GetEmployeeType(string employeeTypeId)
    {
        const string methodName = nameof(GetEmployeeType);

        _logger.LogInformation(
            "[{Repository}].[{Method}] - Execution started successfully with input : {EmployeeTypeId}",
            RepositoryName, methodName, employeeTypeId);

        var connection = _connectionFactory.CreateConnection();

        _logger.LogDebug("[{Repository}].[{Method}] - Established connection to Database Successfully", RepositoryName,
            methodName);

        var employeeType = await connection.QueryFirstOrDefaultAsync<EmployeeType>(
            sql: EmployeeQueries.GetEmployeeTypeByIdQuery,
            param: new { EmployeeTypeId = employeeTypeId },
            commandType: CommandType.Text,
            commandTimeout: 10);

        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@EmployeeType}",
            RepositoryName, methodName, employeeType);

        return employeeType;
    }
    
    /// <inheritdoc cref="IEmployeeReadRepository.GetEmployeeTypeByName"/>
    public async Task<EmployeeType> GetEmployeeTypeByName(string employeeTypeName)
    {
        const string methodName = nameof(GetEmployeeTypeByName);

        _logger.LogInformation(
            "[{Repository}].[{Method}] - Execution started successfully with input : {EmployeeTypeId}",
            RepositoryName, methodName, employeeTypeName);

        var connection = _connectionFactory.CreateConnection();

        _logger.LogDebug("[{Repository}].[{Method}] - Established connection to Database Successfully", RepositoryName,
            methodName);

        var employeeType = await connection.QueryFirstOrDefaultAsync<EmployeeType>(
            sql: EmployeeQueries.GetEmployeeTypeByNameQuery,
            param: new { EmployeeTypeName = employeeTypeName },
            commandType: CommandType.Text,
            commandTimeout: 10);

        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@EmployeeType}",
            RepositoryName, methodName, employeeType);

        return employeeType;
    }

    /// <inheritdoc cref="IEmployeeReadRepository.GetEmployeeTypes"/>
    public async Task<List<EmployeeType>> GetEmployeeTypes()
    {
        const string methodName = nameof(GetEmployeeTypes);

        _logger.LogInformation("[{Repository}].[{Method}] - Execution started successfully", RepositoryName,
            methodName);
        
        var connection = _connectionFactory.CreateConnection();
        
        _logger.LogDebug("[{Repository}].[{Method}] - Established connection to Database Successfully", RepositoryName,
            methodName);
        
        var employeeTypes = (await connection.QueryAsync<EmployeeType>(
            sql: EmployeeQueries.GetEmployeeTypesQuery,
            commandType: CommandType.Text,
            commandTimeout: 10)).ToList();
        
        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@EmployeeTypes}",
            RepositoryName, methodName, employeeTypes);
        
        return employeeTypes;
    }
    
    #endregion
}