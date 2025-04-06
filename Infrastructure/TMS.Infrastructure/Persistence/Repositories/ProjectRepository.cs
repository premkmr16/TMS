using TMS.Core.Entities;
using TMS.Core.Interfaces;

namespace TMS.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    public Task<List<Project>> GetProjectDetails()
    {
        throw new NotImplementedException();
    }

    public Task<Project> GetProjectDetail(string projectId)
    {
        throw new NotImplementedException();
    }

    public Task AddProject(Project project)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProject(Project project)
    {
        throw new NotImplementedException();
    }

    public Task AddProjectMember(ProjectMember projectMember)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProjectMember(ProjectMember projectMember)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProjectMember>> GetProjectMembers(string projectId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProjectRole>> GetProjectRoles(string projectId)
    {
        throw new NotImplementedException();
    }
}