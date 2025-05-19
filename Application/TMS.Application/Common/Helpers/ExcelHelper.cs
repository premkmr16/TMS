using TMS.Application.Common.Models;

namespace TMS.Application.Common.Helpers;

/// <summary>
/// 
/// </summary>
public class ExcelHelper : IExcelHelper
{
    /// <inheritdoc cref="IExcelHelper.BuildExcelRequest{T}"/>
    public ExportExcelRequest BuildExcelRequest<T>(
        List<string> headers, Func<T, List<string>> mapToRow, List<T> data, string workSheetName, string fileName)
    {
        return new ExportExcelRequest
        {
            Headers = headers,
            Data = data.Select(mapToRow).ToList(),
            FileName = fileName,
            WorkSheetName = workSheetName,
        };
    }

    /// <inheritdoc cref="IExcelHelper.BuildDefaultExcelPaginationRequest"/>
    public PaginationRequest BuildDefaultExcelPaginationRequest(string sortField)
    {
        return new PaginationRequest
        {
            SortField = sortField,
            SortDirection = "ASC",
            PageSize = 0,
            PageNumber = 0,
            FetchWithPagination = false
        };
    }
}