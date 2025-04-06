namespace TMS.Application.Features.Employees.Contracts.Create;

/// <summary>
/// Represents a request model to create new Employee.
/// </summary>
public class CreateEmployeeRequest : BaseEmployeeContract
{
    /// <summary>
    /// Gets or Sets the UniqueId of EmployeeType.
    /// <example>01JM6XD97M4S0Y7WC1RXYQJMHS</example>
    /// </summary>
    public Ulid EmployeeTypeId { get; set; }
    
    /// <summary>
    /// Gets or sets the Verification Status of Employee Details.
    /// <example>true</example>
    /// </summary>
    public bool IsVerified { get; set; }
}