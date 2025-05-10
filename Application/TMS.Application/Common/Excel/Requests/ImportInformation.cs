using MediatR;
using Microsoft.AspNetCore.Http;

namespace TMS.Application.Common.Excel.Requests;

/// <summary>
/// 
/// </summary>
/// <param name="ExcelFile"></param>
public record ExtractInformation(IFormFile ExcelFile) : IRequest<string>;