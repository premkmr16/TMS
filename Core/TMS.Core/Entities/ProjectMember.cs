namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="ProjectMember"/> Table in the Database.
/// Defines the Information of Employees allocated in Project.
/// </summary>
public sealed class ProjectMember : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for ProjectMember.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public Ulid Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier of Project.
    /// <example>01JM6XD97M4S0Y7WC1RXYQJMHS</example>
    /// </summary>
    public Ulid ProjectId { get; set; }
    
    /// <summary>
    /// Gets or Sets the Project Details.
    /// </summary>
    public Project Project { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier of Employee.
    /// <example>01JMAE893SDEJZX5K190SNA6V8</example>
    /// </summary>
    public Ulid EmployeeId { get; set; }
    
    /// <summary>
    /// Gets or Sets the Employee Details.
    /// </summary>
    public Employee Employee { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier of Project Role.
    /// <example>01JMAFJXZN4Y27C56XDZYDKX11</example>
    /// </summary>
    public Ulid RoleId { get; set; }
    
    /// <summary>
    /// Gets or Sets the ProjectRole Details.
    /// </summary>
    public ProjectRole Role { get; set; }
    
    /// <summary>
    /// Gets or Sets the Allocated Employee Start Date in Project.
    /// <example>2024-06-16 12:02:10</example>
    /// </summary>
    public DateTimeOffset StartDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the Allocated Employee End Date in Project.
    /// <example>2025-11-17 09:12:30</example>
    /// </summary>
    public DateTimeOffset EndDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the Allocated Employee Email Address in Project.
    /// <example>johndoe@gmail.com</example>
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Gets or Sets the Allocated Employee is Active or InActive in Project.
    /// <example>true</example>
    /// </summary>
    public bool IsActive { get; set; }
}