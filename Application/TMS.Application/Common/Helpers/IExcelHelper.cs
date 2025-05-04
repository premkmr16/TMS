using TMS.Application.Common.Models;

namespace TMS.Application.Common.Helpers;

public interface IExcelHelper
{
    public ExportExcelRequest BuildExcelRequest<T>(
        List<string> headers, Func<T, List<string>> mapToRow, List<T> data, string workSheetName, string fileName);

    public PaginationRequest BuildDefaultExcelPaginationRequest(string sortField);
}
