namespace TMS.Application.Features.Employees.Contracts;

public class BaseEmployeeContract
{
    /// <summary>
    /// Gets or sets the Employee Name.
    /// <example>John Doe</example>
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Contact Number.
    /// <example>8719106611</example>
    /// </summary>
    public string Phone { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Date of Birth.
    /// <example>11-04-2000</example>
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// Gets or Sets the UniqueId of EmployeeType.
    /// <example>01JM6XD97M4S0Y7WC1RXYQJMHS</example>
    /// </summary>
    public string EmployeeTypeId { get; set; }
    
    /// <summary>
    /// Gets or Sets the start date of employee.
    /// <example>2024-09-25 02:55:03</example>
    /// </summary>
    public DateTimeOffset StartDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the end date of employee.
    /// <example>2026-10-27 08:27:25</example>
    /// </summary>
    public DateTimeOffset EndDate { get; set; }
}