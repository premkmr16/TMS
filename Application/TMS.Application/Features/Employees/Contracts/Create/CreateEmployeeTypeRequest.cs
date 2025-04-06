namespace TMS.Application.Features.Employees.Contracts.Create;

/// <summary>
/// Represents a request model to create new EmployeeType.
/// </summary>
public class CreateEmployeeTypeRequest
{
    /// <summary>
    /// Gets or Sets the Type of Employee.
    /// <example>Intern</example>
    /// </summary>
    public string Type { get; set; }
}