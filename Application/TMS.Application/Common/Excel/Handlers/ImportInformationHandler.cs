using System.Globalization;
using System.Text.Json;
using ClosedXML.Excel;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Excel.Requests;

namespace TMS.Application.Common.Excel.Handlers;

/// <summary>
/// 
/// </summary>
public class ExtractInformationHandler : IRequestHandler<ExtractInformation, string>
{
    #region Fields
    
    /// <summary>
    /// 
    /// </summary>
    private const string HandlerName = nameof(ExtractInformationHandler);
    
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<ExtractInformationHandler> _logger;
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public ExtractInformationHandler(ILogger<ExtractInformationHandler> logger)
    {
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
    public async Task<string> Handle(ExtractInformation request, CancellationToken cancellationToken)
    {
        const string methodName = nameof(Handle);
        
        var data = new List<Dictionary<string, string>>();
        
        var stream = new MemoryStream();
        await request.ExcelFile.CopyToAsync(stream, cancellationToken);
        stream.Position = 0;
        
        var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheets.First();
        var rows = worksheet.RangeUsed()!.RowsUsed();

        var text = CultureInfo.CurrentCulture.TextInfo;
        var headerRow = rows.First();
        var headers = headerRow.Cells()
            .Select(header => text.ToTitleCase(header.Value.ToString()).Replace(" ", ""))
            .ToList();

        foreach (var row in rows.Skip(1))
        {
            var rowData = new Dictionary<string, string>();
            foreach (var cell in row.Cells())
            {
                var header = headers[cell.Address.ColumnNumber - 1];
                rowData[header] = cell.Value.ToString();
            }
            data.Add(rowData);
        }
        
        var result =  JsonSerializer.Serialize(
            data, new JsonSerializerOptions { WriteIndented = true });

        return result;
    }
    
    #endregion
}