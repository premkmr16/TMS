using System.Text.Json;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Excel.Requests;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Queries.Requests;
using TMS.Application.Repositories.EmployeeRepository;

namespace TMS.Application.Features.Employees.Queries.Handlers;

/// <summary>
/// 
/// </summary>
public class ImportEmployeeHandler : IRequestHandler<ImportEmployees>
{
    #region Fields

    /// <summary>
    /// 
    /// </summary>
    private const string HandlerName = nameof(ImportEmployeeHandler);

    /// <summary>
    /// 
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    private readonly IEmployeeWriteRepository _employeeWriteRepository;

    /// <summary>
    /// 
    /// </summary>
    private readonly IEmployeeReadRepository _employeeReadRepository;

    /// <summary>
    /// 
    /// </summary>
    private readonly IValidator<ImportEmployee> _importEmployeeRequestValidator;

    /// <summary>
    /// 
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<ImportEmployeeHandler> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="mapper"></param>
    /// <param name="employeeWriteRepository"></param>
    /// <param name="employeeReadRepository"></param>
    /// <param name="importEmployeeRequestValidator"></param>
    /// <param name="logger"></param>
    public ImportEmployeeHandler(
        IMediator mediator,
        IMapper mapper,
        IEmployeeWriteRepository employeeWriteRepository,
        IEmployeeReadRepository employeeReadRepository,
        IValidator<ImportEmployee> importEmployeeRequestValidator,
        ILogger<ImportEmployeeHandler> logger)
    {
        _mediator = mediator;
        _employeeWriteRepository = employeeWriteRepository;
        _employeeReadRepository = employeeReadRepository;
        _importEmployeeRequestValidator = importEmployeeRequestValidator;
        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    #region Handlers

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(ImportEmployees request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);

        _logger.LogInformation("[{Handler}].[{Method}] - Execution started successfully with file name : {FileName}",
            HandlerName, methodName, request.File.FileName);

        var excelData = await _mediator.Send(new ExtractInformation(request.File), cancellationToken);
        var importRequest = new ImportEmployee { ImportEmployeeRequests = JsonSerializer.Deserialize<List<ImportEmployeeRequest>>(excelData) };

        var employeeTypes = await _employeeReadRepository.GetEmployeeTypes();
        
        var allEmployeeTypes = employeeTypes.Select(x => x.Type).ToList();
        var specialEmployeeTypes = employeeTypes
            .Where(x => x.Type is "Contractor" or "Intern").Select(x => x.Type)
            .ToList();

        var importEmployeeRequestValidationResult = await _importEmployeeRequestValidator.ValidateAsync(
            new ValidationContext<ImportEmployee>(importRequest)
            {
                RootContextData =
                {
                    ["employeeTypes"] = allEmployeeTypes,
                    ["specialEmployeeTypes"] = specialEmployeeTypes
                }
            }, cancellationToken);
        
        if (!importEmployeeRequestValidationResult.IsValid)
            throw new ValidationException(importEmployeeRequestValidationResult.Errors);

        _logger.LogInformation("[{Handler}].[{Method}] - Execution completed successfully", HandlerName, methodName);
    }

    #endregion
}