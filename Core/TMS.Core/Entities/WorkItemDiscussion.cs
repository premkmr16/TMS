namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="WorkItemDiscussion"/> Table in the Database.
/// Defines the Comments added by Users in WorkItem.
/// </summary>
public sealed class WorkItemDiscussion : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for WorkItem Discussion.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier of WorkItem.
    /// <example>01JM6XD97M4S0Y7WC1RXYQJMHS</example>
    /// </summary>
    public string WorkItemId { get; set; }
    
    /// <summary>
    /// Gets or Sets the WorkItem Details.
    /// </summary>
    public WorkItem WorkItem { get; set; }
    
    /// <summary>
    /// Gets or Sets the comments in WorkItem.
    /// <example>The WorkItem is ready to review.</example>
    /// </summary>
    public string Discussion { get; set; }
}