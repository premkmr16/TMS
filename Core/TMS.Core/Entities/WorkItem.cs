namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="WorkItem"/> Table in the Database.
/// Defines the WorkItem Details of Project.
/// </summary>
public sealed class WorkItem : TrackableEntity
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for WorkItem.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier Integer for WorkItem.
    /// <example>187191</example>
    /// </summary>
    public int WorkItemId { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier of Project Member.
    /// <example>01JM6XD97M4S0Y7WC1RXYQJMHS</example>
    /// </summary>
    public string MemberId { get; set; }
    
    /// <summary>
    /// Gets or Sets the Project Member Details.
    /// </summary>
    public ProjectMember Member { get; set; }
    
    /// <summary>
    /// Gets or Sets the WorkItem Title.
    /// <example>Create a UI to display all employee details</example>
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Gets or Sets the Priority Details.
    /// </summary>
    public Priority Priority { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier of Priority.
    /// <example>01JMAE893SDEJZX5K190SNA6V8</example>
    /// </summary>
    public string PriorityId { get; set; }
    
    /// <summary>
    /// Gets or Sets the Status Details.
    /// </summary>
    public Status Status { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier of Status.
    /// <example>01JMAFJXZN4Y27C56XDZYDKX11</example>
    /// </summary>
    public string StatusId { get; set; }
    
    /// <summary>
    /// Gets or Sets the Category Details.
    /// </summary>
    public Category Category { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier of Category.
    /// <example>01JMBBYXZP1PR67EFEHTK9JQCJ</example>
    /// </summary>
    public string CategoryId { get; set; }
    
    /// <summary>
    /// Gets or Sets the WorkItem Business Requirement Information.
    /// <example>Create a table in employee component file to display employee data</example>
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Gets or Sets the WorkItem Acceptance Criteria.
    /// <example>UI should display all employee details</example>
    /// </summary>
    public string AcceptanceCriteria { get; set; }

    /// <summary>
    /// Gets or Sets the Unique Identifier of WorkItem
    /// <example></example>
    /// </summary>
    public string ParentWorkItemId { get; set; }
    
    /// <summary>
    /// Gets or Sets the days to complete WorkItem.
    /// <example>2</example>
    /// </summary>
    public int StoryPoints { get; set; }
    
    /// <summary>
    /// Gets or Sets the Tags for WorkItem.
    /// <example>Employee UI</example>
    /// </summary>
    public string Tags { get; set; }
}