namespace TMS.Application.Features.Employees.Contracts.Create;

/// <summary>
/// Represents a request model to create new Employee.
/// </summary>
public class CreateEmployeeRequest : BaseEmployeeContract
{
    /// <summary>
    /// Gets or sets the Employee Name.
    /// <example>819101</example>
    /// </summary>
    public string EmployeeNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the Employee EmailAddress.
    /// <example>johndoe@gmail.com</example>
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Gets or sets the Verification Status of Employee Details.
    /// <example>true</example>
    /// </summary>
    public bool IsVerified { get; set; }
}