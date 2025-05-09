using ClosedXML.Excel;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Excel.Requests;

namespace TMS.Application.Common.Excel.Handlers;

/// <summary>
/// Handles the process to export all the information to excel.
/// </summary>
public class ExportInformationHandler : IRequestHandler<ExportInformation, byte[]>
{
    #region Fields
    
    /// <summary>
    /// The name of the handler used for logging.
    /// </summary>
    private const string HandlerName = nameof(ExportInformationHandler);
    
    /// <summary>
    /// Defines the  Logger instance for capturing <see cref="ExportInformationHandler"/> logs.
    /// </summary>
    private readonly ILogger<ExportInformationHandler> _logger;
    
    #endregion
    
    #region Constructors

    /// <summary>
    ///  Initializes the new instance of <see cref="ExportInformationHandler"/>
    /// </summary>
    /// <param name="logger">efines the logger instance of <see cref="ExportInformationHandler"/>.</param>
    public ExportInformationHandler(ILogger<ExportInformationHandler> logger)
    {
        _logger = logger;
    }
    
    #endregion

    #region Handler
    
    /// <summary>
    /// The Handler method implements the functionality to export all the information to excel.
    /// </summary>
    /// <param name="request">he request contains Headers and data to exported to excel</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests</param>
    /// <returns>The byte array that is to be stored in Excel file</returns>
    public async Task<byte[]> Handle(ExportInformation request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);
        
        _logger.LogInformation("[{Handler}].[{Method}] - Execution started successfully with input : {ExportRequest}", 
            HandlerName, methodName, request.ExcelRequest);
        
        var workBook = new XLWorkbook();
        var worksheet = workBook.Worksheets.Add(request.ExcelRequest.WorkSheetName);

        var currentRow = 1;
        worksheet.Row(currentRow).Height = 25.0;
        
        var headerRow = worksheet.Range(currentRow, 1, currentRow, request.ExcelRequest.Headers.Count);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerRow.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        
        for (var column = 0; column < request.ExcelRequest.Headers.Count; column++)
            worksheet.Cell(currentRow, column + 1).Value = request.ExcelRequest.Headers[column];
        
        foreach (var data in request.ExcelRequest.Data)
        {
            currentRow++;
            
            var row = worksheet.Row(currentRow);    
            row.Height = 20.0;
            row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            for (var column = 0; column < data.Count; column++)
            {
                var cell = worksheet.Cell(currentRow, column + 1);
                cell.Value = data[column];
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
        }
        
        worksheet.Columns().AdjustToContents();
        
        var stream = new MemoryStream();
        workBook.SaveAs(stream);
        var content = stream.ToArray();
        
        _logger.LogInformation("[{Handler}].[{Method}] - Execution completed successfully", HandlerName, methodName);

        return await Task.FromResult(content);
    }
    
    #endregion
}