using TMS.Core.Entities;

namespace TMS.Application.Repositories.WorkItemRepository;

/// <summary>
/// 
/// </summary>
public interface IWorkItemWriteRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="workItem"></param>
    /// <returns></returns>
    public Task<WorkItem> AddWorkItem(WorkItem workItem);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="workItem"></param>
    /// <returns></returns>
    public Task<WorkItem> UpdateWorkItem(WorkItem workItem);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="workItemDiscussion"></param>
    /// <returns></returns>
    public Task<WorkItemDiscussion> UpdateWorkItemDiscussion(WorkItemDiscussion workItemDiscussion);
}