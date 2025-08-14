using MediatR;
using Microsoft.AspNetCore.Http;

namespace TMS.Application.Features.Employees.Commands.Requests;

/// <summary>
/// The Command to get all the employee information in Excel and store it in database.
/// </summary>
/// <param name="File">The File contains details of new employees information.</param>
public record ImportEmployees(IFormFile File) : IRequest;
