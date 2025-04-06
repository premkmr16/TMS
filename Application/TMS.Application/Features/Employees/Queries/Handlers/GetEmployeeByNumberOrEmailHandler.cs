using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Features.Employees.Queries.Requests;
using TMS.Application.Repositories.EmployeeRepository;

namespace TMS.Application.Features.Employees.Queries.Handlers;

/// <summary>
/// Handles the process to get the Employee by EmployeeNumber or EmailAddress.
/// </summary>
public class GetEmployeeByNumberOrEmailHandler : IRequestHandler<GetEmployeeByNumberOrEmail, EmployeeResponse>
{
    #region Fields
    
    /// <summary>
    /// The name of the handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(GetEmployeeByNumberOrEmailHandler);

    /// <summary>
    /// Defines the Employee Repository for performing employee related write operations.
    /// </summary>
    private readonly IEmployeeReadRepository _employeeReadRepository;

    /// <summary>
    /// Defines the  Logger instance for capturing <see cref="GetEmployeesHandler"/> logs.
    /// </summary>
    private readonly ILogger<GetEmployeeByNumberOrEmailHandler> _logger;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Initializes the new instance of <see cref="GetEmployeeByNumberOrEmailHandler"/>
    /// </summary>
    /// <param name="employeeReadRepository">Defines the Employee Repository <see cref="IEmployeeReadRepository"/>.</param>
    /// <param name="logger">Defines the logger instance of <see cref="GetEmployeeByNumberOrEmailHandler"/></param>
    public GetEmployeeByNumberOrEmailHandler(
        IEmployeeReadRepository employeeReadRepository,
        ILogger<GetEmployeeByNumberOrEmailHandler> logger)
    {
        _employeeReadRepository = employeeReadRepository;
        _logger = logger;
    }
    
    #endregion
    
    #region Handler

    /// <summary>
    /// The Handler method implements the functionality to get the employee by EmployeeNumber or EmailAddress.
    /// </summary>
    /// <param name="request">The request contains Employee Number or EmailAddress or both.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>Returns <see cref="EmployeeResponse"/> which contains employee data.</returns>
    public async Task<EmployeeResponse> Handle(GetEmployeeByNumberOrEmail request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);

        _logger.LogInformation(
            "[{Handler}].[{Method}] - Execution started successfully with input EmployeeNumber : {EmployeeNUmber} and EmailAddress : {EmailAddress}",
            HandlerName, methodName, request.EmployeeNumber, request.EmailAddress);

        var employee =
            await _employeeReadRepository.GetEmployeeByNumberOrEmail(request.EmployeeNumber, request.EmailAddress);

        var employeeResponse = employee.SingleOrDefault(e => 
            e.EmployeeNumber == request.EmployeeNumber && e.Email == request.EmailAddress);

        _logger.LogInformation(
            "[{Handler}].[{Method}] - Execution completed successfully with output : {@EmployeeResponse}",
            HandlerName, methodName, employeeResponse);

        return employeeResponse;
    }
    
    #endregion
}