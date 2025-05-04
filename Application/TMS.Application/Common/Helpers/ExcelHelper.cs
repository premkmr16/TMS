using TMS.Application.Common.Models;

namespace TMS.Application.Common.Helpers;

public class ExcelHelper : IExcelHelper
{
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