using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.DataHandler;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Features.Employees.Queries.Requests;
using TMS.Application.Repositories.EmployeeRepository;

namespace TMS.Application.Features.Employees.Queries.Handlers;

/// <summary>
/// Handles the process to get the Employee using Unique Identifier of Employee.
/// </summary>
public class GetEmployeeHandler : IRequestHandler<GetEmployee, EmployeeResponse>
{
    #region Fields

    /// <summary>
    /// The name of the handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(GetEmployeeHandler);

    /// <summary>
    /// Defines the Employee Repository for performing employee related read operations.
    /// </summary>
    private readonly IEmployeeReadRepository _employeeReadRepository;
    
    /// <summary>
    /// 
    /// </summary>
    private readonly IDataHandler _dataHandler;

    /// <summary>
    /// Defines the  Logger instance for capturing <see cref="GetEmployeeHandler"/> logs.
    /// </summary>
    private readonly ILogger<GetEmployeeHandler> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes the new instance of <see cref="GetEmployeeHandler"/>
    /// </summary>
    /// <param name="employeeReadRepository">Defines the Employee Repository <see cref="IEmployeeReadRepository"/>.</param>
    /// <param name="dataHandler"></param>
    /// <param name="logger">Defines the logger instance of <see cref="GetEmployeeHandler"/></param>
    public GetEmployeeHandler(
        IEmployeeReadRepository employeeReadRepository,
        IDataHandler dataHandler,
        ILogger<GetEmployeeHandler> logger)
    {
        _employeeReadRepository = employeeReadRepository;
        _dataHandler = dataHandler;
        _logger = logger;
    }

    #endregion
    
    #region Handler

    /// <summary>
    /// The Handler method implements the functionality to get the employee by Unique Identifier of Employee.
    /// </summary>
    /// <param name="request">The request contains Employee Unique Identifier</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>Returns <see cref="EmployeeResponse"/> which contains employee data.</returns>
    public async Task<EmployeeResponse> Handle(GetEmployee request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);

        _logger.LogInformation(
            "[{Handler}].[{Method}] - Execution started successfully with input EmployeeId : {EmployeeId}", HandlerName,
            methodName, request.EmployeeId);
        
        if (!Ulid.TryParse(request.EmployeeId, out var employeeId) || employeeId == Ulid.Empty)
            throw new ArgumentException($"Employee Id {request.EmployeeId} is not in valid format");
        
        var employee = await _dataHandler.GetOrLoadAsync(
            request.EmployeeId, 
            () => _employeeReadRepository.GetEmployee(request.EmployeeId));
        
        _logger.LogInformation(
            "[{Handler}].[{Method}] - Execution completed successfully with output : {@EmployeeResponse}",
            HandlerName, methodName, employee);
        
        return employee;
    }
    
    #endregion
}