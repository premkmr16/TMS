using TMS.Application.Common.Models;

namespace TMS.Application.Common.Helpers;

/// <summary>
/// Provides helper methods to generate Excel-related data and pagination requests.
/// </summary>
public interface IExcelHelper
{
    /// <summary>
    /// Builds an <see cref="ExportExcelRequest"/> object using the provided headers, data, and mapping function.
    /// </summary>
    /// <typeparam name="T">The type of data being exported.</typeparam>
    /// <param name="headers">A list of column headers for the Excel sheet.</param>
    /// <param name="mapToRow">A function that maps each data item to a list of string values representing a row.</param>
    /// <param name="data">The list of data items to include in the Excel export.</param>
    /// <param name="workSheetName">The name of the worksheet in the Excel file.</param>
    /// <param name="fileName">The desired name of the exported Excel file.</param>
    /// <returns>An <see cref="ExportExcelRequest"/> containing the Excel file structure and metadata.</returns>
    ExportExcelRequest BuildExcelRequest<T>(
        List<string> headers, Func<T, List<string>> mapToRow, List<T> data, string workSheetName, string fileName);

    /// <summary>
    /// Builds a default <see cref="PaginationRequest"/> used for paginating Excel exports, sorted by the specified field.
    /// </summary>
    /// <param name="sortField">The field name to sort the Excel data by.</param>
    /// <returns>A <see cref="PaginationRequest"/> with default pagination settings.</returns>
    PaginationRequest BuildDefaultExcelPaginationRequest(string sortField);
}
