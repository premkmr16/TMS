using TMS.Application.Common.Models;

namespace TMS.Application.Common.Helpers;

/// <summary>
/// 
/// </summary>
public interface IExcelHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="headers"></param>
    /// <param name="mapToRow"></param>
    /// <param name="data"></param>
    /// <param name="workSheetName"></param>
    /// <param name="fileName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ExportExcelRequest BuildExcelRequest<T>(
        List<string> headers, Func<T, List<string>> mapToRow, List<T> data, string workSheetName, string fileName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sortField"></param>
    /// <returns></returns>
    public PaginationRequest BuildDefaultExcelPaginationRequest(string sortField);
}
