namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="Project"/> Table in the Database.
/// Defines the required details of the Project.
/// </summary>
public sealed class Project : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Project.
    /// <example>1JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the Title of Project.
    /// <example>Timesheet Management System</example>
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Gets or Sets the Start Date of Project.
    /// <example>2024-09-25 02:55:03</example>
    /// </summary>
    public DateTimeOffset StartDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the End Date of Project.
    /// <example>2025-12-25 10:55:03</example>
    /// </summary>
    public DateTimeOffset EndDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the Actual Start Date of Project.
    /// <example>2024-06-16 07:35:03</example>
    /// </summary>
    public DateTimeOffset ActualStartDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the Actual End Date of Project.
    /// <example>2026-01-21 11:55:01</example>
    /// </summary>
    public DateTimeOffset ActualEndDate { get; set; }
    
    /// <summary>
    /// Gets or Sets the Project is Active or InActive
    /// <example>true</example>
    /// </summary>
    public bool IsActive { get; set; }
}