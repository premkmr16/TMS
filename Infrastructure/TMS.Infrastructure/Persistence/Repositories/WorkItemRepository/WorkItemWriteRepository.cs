using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Repositories.WorkItemRepository;
using TMS.Core.Entities;
using TMS.Infrastructure.Persistence.Context;

namespace TMS.Infrastructure.Persistence.Repositories.WorkItemRepository;

/// <summary>
/// 
/// </summary>
public class WorkItemWriteRepository : IWorkItemWriteRepository
{
    /// <summary>
    /// 
    /// </summary>
    private const string RepositoryName = nameof(WorkItemWriteRepository);

    /// <summary>
    /// 
    /// </summary>
    private readonly TmsDbContext _tmsDbContext;

    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<WorkItemWriteRepository> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tmsDbContext"></param>
    /// <param name="logger"></param>
    public WorkItemWriteRepository(
        TmsDbContext tmsDbContext,
        ILogger<WorkItemWriteRepository> logger)
    {
        _tmsDbContext = tmsDbContext;
        _logger = logger;
    }

    /// <inheritdoc cref="IWorkItemWriteRepository.AddWorkItem"/>
    public async Task<WorkItem> AddWorkItem(WorkItem workItem)
    {
        const string methodName = nameof(AddWorkItem);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully with input : {@WorkItem}",
            RepositoryName, methodName, workItem);

        var addedItem = await _tmsDbContext.WorkItems.AddAsync(workItem);
        var rowsAffected = await _tmsDbContext.SaveChangesAsync();

        if (rowsAffected <= 0)
        {
            _logger.LogError(
                "{Repository}.{Method} - Changes were not saved to the database for entity: {@Entities}",
                RepositoryName, methodName, addedItem.Entity);

            var errorMessage = $"Failed to save changes for WorkItem: {@workItem} to the database.";

            throw new DbUpdateException(errorMessage);
        }

        _logger.LogInformation(
            "{Repository}.{Method} - Execution completed successfully with {rowsAffected} entity added to the database: {@AddedItem}",
            RepositoryName, methodName, rowsAffected, addedItem.Entity);

        return addedItem.Entity;
    }

    /// <inheritdoc cref="IWorkItemWriteRepository.UpdateWorkItem"/>
    public async Task<WorkItem> UpdateWorkItem(WorkItem workItem)
    {
        const string methodName = nameof(UpdateWorkItem);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully with input : {@WorkItem}",
            RepositoryName, methodName, workItem);
        
        var updatedWorkItem = _tmsDbContext.WorkItems.Update(workItem);
        var rowsAffected = await _tmsDbContext.SaveChangesAsync();

        if (rowsAffected <= 0)
        {
            _logger.LogError(
                "{Repository}.{Method} - Changes were not saved to the database for entity: {@Entities}",
                RepositoryName, methodName, updatedWorkItem.Entity);

            var errorMessage = $"Failed to save changes for WorkItem: {@workItem} to the database.";

            throw new DbUpdateException(errorMessage);
        }

        _logger.LogInformation(
            "{Repository}.{Method} - Execution completed successfully with {rowsAffected} entity updated to the database: {@AddedItem}",
            RepositoryName, methodName, rowsAffected, updatedWorkItem.Entity);

        return updatedWorkItem.Entity;
    }

    /// <inheritdoc cref="IWorkItemWriteRepository.UpdateWorkItemDiscussion"/>
    public async Task<WorkItemDiscussion> UpdateWorkItemDiscussion(WorkItemDiscussion workItemDiscussion)
    {
        const string methodName = nameof(UpdateWorkItemDiscussion);

        _logger.LogInformation(
            "{Repository}.{Method} - Execution started successfully with input : {@WorkItemDiscussion}",
            RepositoryName, methodName, workItemDiscussion);

        var updatedWorkItemDiscussion = _tmsDbContext.WorkItemDiscussions.Update(workItemDiscussion);
        var rowsAffected = await _tmsDbContext.SaveChangesAsync();

        if (rowsAffected <= 0)
        {
            _logger.LogError(
                "{Repository}.{Method} - Changes were not saved to the database for entity: {@Entities}",
                RepositoryName, methodName, updatedWorkItemDiscussion.Entity);

            var errorMessage =
                $"Failed to save changes for WorkItem Discussion: {@workItemDiscussion} to the database.";

            throw new DbUpdateException(errorMessage);
        }

        _logger.LogInformation(
            "{Repository}.{Method} - Execution completed successfully with {rowsAffected} entity updated to the database: {@AddedItem}",
            RepositoryName, methodName, rowsAffected, updatedWorkItemDiscussion.Entity);

        return updatedWorkItemDiscussion.Entity;
    }
}