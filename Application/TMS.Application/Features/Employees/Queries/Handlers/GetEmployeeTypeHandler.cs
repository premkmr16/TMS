using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Features.Employees.Queries.Requests;
using TMS.Application.Repositories.EmployeeRepository;

namespace TMS.Application.Features.Employees.Queries.Handlers;

/// <summary>
/// Handles the process to get the EmployeeType using Unique Identifier.
/// </summary>
public class GetEmployeeTypeHandler : IRequestHandler<GetEmployeeType, EmployeeTypeResponse>
{
    #region Fields
    
    /// <summary>
    /// The name of the handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(GetEmployeeTypeHandler);

    /// <summary>
    /// Defines the Employee Repository for performing employee related read operations.
    /// </summary>
    private readonly IEmployeeReadRepository _employeeReadRepository;

    /// <summary>
    /// Defines the Mapper for transforming object properties between different models.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Defines the  Logger instance for capturing <see cref="GetEmployeeTypeHandler"/> logs.
    /// </summary>
    private readonly ILogger<GetEmployeeTypeHandler> _logger;
    
    #endregion

    #region Constructors
    
    /// <summary>
    /// Initializes the new instance of <see cref="GetEmployeeTypeHandler"/>
    /// </summary>
    /// <param name="employeeReadRepository">Defines the Employee Repository <see cref="IEmployeeReadRepository"/>.</param>
    /// <param name="mapper">Defines the Mapper of EmployeeType <see cref="IMapper"/>.</param>
    /// <param name="logger">Defines the logger instance of <see cref="GetEmployeeTypeHandler"/></param>
    public GetEmployeeTypeHandler(
        IEmployeeReadRepository employeeReadRepository,
        IMapper mapper,
        ILogger<GetEmployeeTypeHandler> logger)
    {
        _employeeReadRepository = employeeReadRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    #endregion

    #region Handler
    
    /// <summary>
    /// The Handler method implements the functionality to add new employee type to database.
    /// </summary>
    /// <param name="request">The request containing unique id of employee type.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>Returns <see cref="EmployeeTypeResponse"/> which contains employee type data.</returns>
    public async Task<EmployeeTypeResponse> Handle(GetEmployeeType request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);

        _logger.LogInformation(
            "[{Handler}].[{Method}] - Execution started successfully with input EmployeeTypeid : {EmployeeTypeId}",
            HandlerName, methodName, request.EmployeeTypeId);

        if (!Ulid.TryParse(request.EmployeeTypeId, out var employeeTypeId) || employeeTypeId == Ulid.Empty)
            throw new ArgumentException($"Employee Type Id {request.EmployeeTypeId} is not in valid format");

        var employeeType = await _employeeReadRepository.GetEmployeeType(request.EmployeeTypeId);
        
        var employeeTypeResponse = _mapper.Map<EmployeeTypeResponse>(employeeType);
        
        _logger.LogInformation(
            "[{Handler}].[{Method}] - Execution completed successfully with output : {@EmployeeResponse}",
            HandlerName, methodName, employeeTypeResponse);     
        
        return employeeTypeResponse;
    }
    
    #endregion
}