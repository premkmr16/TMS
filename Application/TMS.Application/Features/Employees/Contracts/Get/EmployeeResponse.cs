using TMS.Core.Entities;

namespace TMS.Application.Features.Employees.Contracts.Get;

/// <summary>
/// Represents the response model for an Employee entity.
/// </summary>
public class EmployeeResponse : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Employee.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public Ulid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the Employee Name.
    /// <example>819101</example>
    /// </summary>
    public string EmployeeNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the Employee Name.
    /// <example>John Doe</example>
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the Employee EmailAddress.
    /// <example>johndoe@gmail.com</example>
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Contact Number.
    /// <example>8719106611</example>
    /// </summary>
    public string Phone { get; set; }
    
    /// <summary>
    /// Gets or Sets the UniqueId of EmployeeType.
    /// <example>Contractor</example>
    /// </summary>
    public string EmployeeType { get; set; }
    
    /// <summary>
    /// Gets or Sets the employee is active or inactive.
    /// <example>true</example>
    /// </summary>
    public bool IsActive { get; set; }
    
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