using MediatR;
using TMS.Application.Common.Models;

namespace TMS.Application.Common.Excel.Requests;

/// <summary>
/// The Query to export all the information to excel.
/// </summary>
/// <param name="ExcelRequest">The request contains the data to be exported to excel</param>
public record ExportInformation(ExportExcelRequest ExcelRequest) : IRequest<byte[]>;