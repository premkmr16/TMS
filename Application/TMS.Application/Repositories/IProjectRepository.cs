using TMS.Core.Entities;

namespace TMS.Core.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IProjectRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<List<Project>> GetProjectDetails();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    public Task<Project> GetProjectDetail(string projectId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public Task AddProject(Project project);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public Task UpdateProject(Project project);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectMember"></param>
    /// <returns></returns>
    public Task AddProjectMember(ProjectMember projectMember);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectMember"></param>
    /// <returns></returns>
    public Task UpdateProjectMember(ProjectMember projectMember);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    public Task<List<ProjectMember>> GetProjectMembers(string projectId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    public Task<List<ProjectRole>> GetProjectRoles(string projectId);
}