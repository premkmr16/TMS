namespace TMS.Core.Entities;

/// <summary>
/// 
/// </summary>
public sealed class EmployeeRelation : TrackableEntity
{
    /// <summary>
    /// 
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string MentorId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Employee Mentor { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string MenteeId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Employee Mentee { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset StartDate { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset EndDate { get; set; }
}