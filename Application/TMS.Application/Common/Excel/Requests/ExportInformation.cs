using MediatR;
using TMS.Application.Common.Models;

namespace TMS.Application.Common.Excel.Requests;

public record ExportInformation(ExportExcelRequest ExcelRequest) : IRequest<byte[]>;