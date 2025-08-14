using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Models;
using TMS.Application.DataHandler;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Features.Employees.Queries.Requests;
using TMS.Application.Repositories.EmployeeRepository;
using TMS.Core.Entities;

namespace TMS.Application.Features.Employees.Queries.Handlers;

/// <summary>
/// Handles the process to get all Employees.
/// </summary>
public class GetEmployeesHandler : IRequestHandler<GetEmployees, PaginatedResponse<EmployeeResponse>>
{
    #region Fields

    /// <summary>
    /// The name of the handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(GetEmployeesHandler);

    /// <summary>
    /// Defines the Employee Repository for performing employee related read operations.
    /// </summary>
    private readonly IEmployeeReadRepository _employeeReadRepository;

    /// <summary>
    /// 
    /// </summary>
    private readonly IDataHandler _dataHandler;

    /// <summary>
    /// Defines the  Logger instance for capturing <see cref="GetEmployeesHandler"/> logs.
    /// </summary>
    private readonly ILogger<GetEmployeesHandler> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes the new instance of <see cref="GetEmployeesHandler"/>
    /// </summary>
    /// <param name="employeeReadRepository">Defines the Employee Repository <see cref="IEmployeeReadRepository"/>.</param>
    /// <param name="dataHandler"></param>
    /// <param name="logger">Defines the logger instance of <see cref="GetEmployeesHandler"/></param>
    public GetEmployeesHandler(
        IEmployeeReadRepository employeeReadRepository,
        IDataHandler dataHandler,
        ILogger<GetEmployeesHandler> logger)
    {
        _employeeReadRepository = employeeReadRepository;
        _dataHandler = dataHandler;
        _logger = logger;
    }

    #endregion

    #region Handler

    /// <summary>
    /// The Handler method implements the functionality to get all the employees.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>Returns List of Employee Response</returns>
    public async Task<PaginatedResponse<EmployeeResponse>> Handle(GetEmployees request,
        CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);

        _logger.LogInformation("[{Handler}].[{Method}] - Execution started successfully", HandlerName, methodName);
        
        var employees = await _dataHandler.GetOrLoadAsync(
            nameof(Employee), request.PaginationRequest, 
            () => _employeeReadRepository.GetEmployees(request.PaginationRequest));
       
        _logger.LogInformation("[{Handler}].[{Method}] - Execution completed successfully with output : {@Employees}",
            HandlerName, methodName, employees);

        return employees;
    }

    #endregion
}