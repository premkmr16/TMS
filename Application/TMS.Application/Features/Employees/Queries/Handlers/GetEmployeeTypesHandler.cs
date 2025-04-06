using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Features.Employees.Queries.Requests;
using TMS.Application.Repositories.EmployeeRepository;

namespace TMS.Application.Features.Employees.Queries.Handlers;

/// <summary>
/// Handles the process to get all Employee Types.
/// </summary>
public class GetEmployeeTypesHandler : IRequestHandler<GetEmployeeTypes, List<EmployeeTypeResponse>>
{
    #region Fields

    /// <summary>
    /// The name of the handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(GetEmployeeTypesHandler);

    /// <summary>
    /// Defines the Employee Repository for performing employeeType related read operations.
    /// </summary>
    private readonly IEmployeeReadRepository _employeeReadRepository;

    /// <summary>
    /// Defines the Mapper for transforming object properties between different models.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Defines the  Logger instance for capturing <see cref="GetEmployeeTypesHandler"/> logs.
    /// </summary>
    private readonly ILogger<GetEmployeeTypesHandler> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes the new instance of <see cref="GetEmployeeTypesHandler"/>
    /// </summary>
    /// <param name="employeeReadRepository">Defines the Employee Repository <see cref="IEmployeeReadRepository"/>.</param>
    /// <param name="mapper">Defines the Mapper of Employee <see cref="IMapper"/>.</param>
    /// <param name="logger">Defines the logger instance of <see cref="GetEmployeeTypesHandler"/>.</param>
    public GetEmployeeTypesHandler(
        IEmployeeReadRepository employeeReadRepository,
        IMapper mapper,
        ILogger<GetEmployeeTypesHandler> logger)
    {
        _employeeReadRepository = employeeReadRepository;
        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    #region Handler

    /// <summary>
    /// The Handler method implements the functionality to get all the employee types.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>Returns List of Employee Type Response</returns>
    public async Task<List<EmployeeTypeResponse>> Handle(GetEmployeeTypes request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);

        _logger.LogInformation("[{Handler}].[{Method}] - Execution started successfully", HandlerName, methodName);

        var employeeTypes = await _employeeReadRepository.GetEmployeeTypes();

        var employeeTypesResponse = _mapper.Map<List<EmployeeTypeResponse>>(employeeTypes);

        _logger.LogInformation(
            "[{Handler}].[{Method}] - Execution completed successfully with output : {@EmployeeResponse}",
            HandlerName, methodName, employeeTypesResponse);

        return employeeTypesResponse;
    }

    #endregion
}