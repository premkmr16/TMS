using TMS.Core.Entities;

namespace TMS.Application.Repositories.WorkItemRepository;

public interface IWorkItemQueryRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<List<WorkItem>> GetAllWorkItems(string projectId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="workItemId"></param>
    /// <returns></returns>
    public Task<WorkItem> GetWorkItem(string workItemId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<List<Category>> GetCategories();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<List<Priority>> GetPriorities();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<List<Status>> GetWorkItemStatus();
}
