namespace TMS.Core.Entities;

/// <summary>
/// Represents the <see cref="ProjectWorkItem"/> view in the Database.
/// Defines the WorkItem Details of project.
/// </summary>
public class ProjectWorkItem
{
    /// <summary>
    /// Gets or Sets the Unique Identifier for Project.
    /// <example>1JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string ProjectId { get; set; }
    
    /// <summary>
    /// Gets or Sets the Category Type of WorkItem.
    /// <example>Story</example>
    /// </summary>
    public string Category { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier for WorkItem.
    /// <example>01JM6XC67ZMNQN2W3P63RG98KP</example>
    /// </summary>
    public string WorkItemId { get; set; }
    
    /// <summary>
    /// Gets or Sets the Unique Identifier Integer for WorkItem.
    /// <example>187191</example>
    /// </summary>
    public int WorkItemNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the Employee Name.
    /// <example>John Doe</example>
    /// </summary>
    public string AssignedTo { get; set; }
    
    /// <summary>
    /// Gets or Sets the WorkItem Title.
    /// <example>Create a UI to display all employee details</example>
    /// </summary>
    public string Title { get; set; }
    
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
    /// Gets or Sets the WorkItem Status
    /// <example>In Development</example>.
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Gets or Sets the Tags for WorkItem.
    /// <example>Employee UI</example>
    /// </summary>
    public string Tags { get; set; }
    
    /// <summary>
    /// Gets or Sets the days to complete WorkItem.
    /// <example>2</example>
    /// </summary>
    public int StoryPoints { get; set; }
    
    /// <summary>
    /// Gets or Sets the Priority Type of WorkItem.
    /// <example>High</example>
    /// </summary>
    public string Priority { get; set; }
}