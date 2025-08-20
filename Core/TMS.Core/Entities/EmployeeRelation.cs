namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="EmployeeRelation"/> Table in the Database.
/// Defines the mentor and mentee relation within the organization.
/// </summary>
public sealed class EmployeeRelation : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Employee Relation.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the UniqueId of Employee.
    /// <example>01JM6XD97M4S0Y7WC1RXYQJMHS</example>
    /// </summary>
    public string MentorId { get; set; }
    
    /// <summary>
    /// Gets or sets the mentor for employee
    /// </summary>
    public Employee Mentor { get; set; }
    
    /// <summary>
    /// Gets or Sets the UniqueId of Employee.
    /// <example>01JM6XD97M4S0Y7WC1RXYQJMHS</example>
    /// </summary>
    public string MenteeId { get; set; }
    
    /// <summary>
    /// Gets or sets the mentee for employee
    /// </summary>
    public Employee Mentee { get; set; }
    
    /// <summary>
    /// Gets or Sets the relation is active or inactive.
    /// <example>true</example>
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Gets or Sets the start date of employee relation.
    /// <example>2024-09-25 02:55:03</example>
    /// </summary>
    public DateTimeOffset StartDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the end date of employee relation.
    /// <example>2026-10-27 08:27:25</example>
    /// </summary>
    public DateTimeOffset EndDate { get; set; }
}