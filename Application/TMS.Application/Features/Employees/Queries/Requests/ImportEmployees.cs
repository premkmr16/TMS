using MediatR;
using Microsoft.AspNetCore.Http;

namespace TMS.Application.Features.Employees.Queries.Requests;

/// <summary>
/// 
/// </summary>
/// <param name="File"></param>
public record ImportEmployees(IFormFile File) : IRequest;
