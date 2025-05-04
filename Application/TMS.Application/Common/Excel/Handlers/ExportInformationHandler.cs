using ClosedXML.Excel;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Excel.Requests;

namespace TMS.Application.Common.Excel.Handlers;

/// <summary>
/// 
/// </summary>
public class ExportInformationHandler : IRequestHandler<ExportInformation, byte[]>
{
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<ExportInformationHandler> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public ExportInformationHandler(ILogger<ExportInformationHandler> logger)
    {
        _logger = logger;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<byte[]> Handle(ExportInformation request, CancellationToken cancellationToken)
    {
        var workBook = new XLWorkbook();
        var worksheet = workBook.Worksheets.Add(request.ExcelRequest.WorkSheetName);

        var currentRow = 1;
        worksheet.Row(currentRow).Height = 25.0;
        
        var headerRow = worksheet.Range(currentRow, 1, currentRow, request.ExcelRequest.Headers.Count);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerRow.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        
        for (var i = 0; i < request.ExcelRequest.Headers.Count; i++)
            worksheet.Cell(currentRow, i + 1).Value = request.ExcelRequest.Headers[i];
        
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

        return await Task.FromResult(content);
    }
}