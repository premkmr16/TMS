using MediatR;
using Microsoft.AspNetCore.Http;

namespace TMS.Application.Common.Excel.Requests;

/// <summary>
/// The Query to extract all the information from Excel.
/// </summary>
/// <param name="ExcelFile">The file contains the information to be extracted.</param>
public record ExtractInformation(IFormFile ExcelFile) : IRequest<string>;