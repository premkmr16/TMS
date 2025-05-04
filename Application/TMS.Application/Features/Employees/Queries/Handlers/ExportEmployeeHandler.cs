using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Excel.Requests;
using TMS.Application.Common.Helpers;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Features.Employees.Queries.Requests;
using static TMS.Core.Common.ExcelConstants;

namespace TMS.Application.Features.Employees.Queries.Handlers;

/// <summary>
/// 
/// </summary>
public class ExportEmployeeHandler : IRequestHandler<ExportEmployees, byte[]>
{

    #region Fields
    
    /// <summary>
    /// 
    /// </summary>
    private const string HandlerName = nameof(ExportEmployeeHandler);
    
    /// <summary>
    /// 
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    private readonly IExcelHelper _excelHelper;

    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<ExportEmployeeHandler> _logger;
    
    #endregion
    
    #region Contructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    /// <param name="excelHelper"></param>
    public ExportEmployeeHandler(
        IMediator mediator,
        ILogger<ExportEmployeeHandler> logger,
        IExcelHelper excelHelper)
    {
        _mediator = mediator;
        _logger = logger;
        _excelHelper = excelHelper;
    }
    
    #endregion
    
    #region Handlers

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<byte[]> Handle(ExportEmployees request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);
        
        _logger.LogInformation("[{Handler}].[{Method}] - Execution started successfully", HandlerName, methodName);
        
        var paginationRequest = _excelHelper.BuildDefaultExcelPaginationRequest(nameof(EmployeeResponse.Id));
       
        var employeeResponse = await _mediator.Send(new GetEmployees(paginationRequest), cancellationToken);

        var exportExcelRequest = _excelHelper.BuildExcelRequest(
            Employee.EmployeeExcelHeaders, 
            employee => [
                employee.EmployeeNumber,
                employee.Name,
                employee.Email,
                employee.Phone,
                employee.DateOfBirth.Date.ToString(Excel.DateFormat),
                employee.Type,
                employee.StartDate.Date.ToString(Excel.DateFormat),
                employee.EndDate is not null
                    ? employee.EndDate.Value.Date.ToString(Excel.DateFormat)
                    : string.Empty
            ], 
            employeeResponse.Data, 
            Employee.WorkSheetName, 
            Employee.FileName);

        var excelBytes = await _mediator.Send(new ExportInformation(exportExcelRequest), cancellationToken);
        
        _logger.LogInformation("[{Handler}].[{Method}] - Execution completed successfully", HandlerName, methodName);     
        
        return excelBytes;
    }
    
    #endregion
}