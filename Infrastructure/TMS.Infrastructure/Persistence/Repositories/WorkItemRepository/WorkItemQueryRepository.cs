using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using TMS.Application.ConnectionFactory;
using TMS.Application.Repositories.WorkItemRepository;
using TMS.Core.Database;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.Repositories.WorkItemRepository;

public class WorkItemQueryRepository : IWorkItemQueryRepository
{
    private const string RepositoryName = nameof(WorkItemWriteRepository);

    /// <summary>
    /// 
    /// </summary>
    private readonly IConnectionFactory _connectionFactory;

    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<WorkItemQueryRepository> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionFactory"></param>
    /// <param name="logger"></param>
    public WorkItemQueryRepository(IConnectionFactory connectionFactory, ILogger<WorkItemQueryRepository> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    /// <inheritdoc cref="IWorkItemQueryRepository.GetAllWorkItems"/>
    public async Task<List<WorkItem>> GetAllWorkItems(string projectId)
    {
        const string methodName = nameof(GetAllWorkItems);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully", RepositoryName, methodName);

        var connection = _connectionFactory.CreateConnection();
        
        if (connection.State != ConnectionState.Open)
        {
            _logger.LogError("{Repository}.{Method} - Failed to establish a database connection", RepositoryName,
                methodName);
            
            const string errorMessage = "Failed to establish a database connection";
            throw new InvalidOperationException(errorMessage);
        }

        _logger.LogDebug("{Repository}.{Method} - Established connection to Database Successfully", RepositoryName,
            methodName);

        var workItems =
            (await connection.QueryAsync<WorkItem>(
                sql: WorkItemQueries.GetWorkItemQuery,
                commandType: CommandType.Text,
                commandTimeout: 60))
            .ToList();

        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@WorkItems}",
            RepositoryName, methodName, workItems);

        return workItems;
    }

    /// <inheritdoc cref="IWorkItemQueryRepository.GetWorkItem"/>
    public async Task<WorkItem> GetWorkItem(string workItemId)
    {
        const string methodName = nameof(GetWorkItem);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully", RepositoryName, methodName);

        var connection = _connectionFactory.CreateConnection();

        _logger.LogDebug("{Repository}.{Method} - Established connection to Database Successfully", RepositoryName,
            methodName);

        var workItem = await connection.QueryFirstOrDefaultAsync<WorkItem>(
            sql: WorkItemQueries.GetWorkItemByIdQuery,
            commandType: CommandType.Text,
            commandTimeout: 60);

        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@WorkItem}",
            RepositoryName, methodName, workItem);

        return workItem;
    }

    /// <inheritdoc cref="IWorkItemQueryRepository.GetCategories"/>
    public async Task<List<Category>> GetCategories()
    {
        const string methodName = nameof(GetCategories);

        _logger.LogInformation("{Repository}.{Method} - Execution started successfully", RepositoryName, methodName);

        var connection = _connectionFactory.CreateConnection();

        _logger.LogDebug("{Repository}.{Method} - Established connection to Database Successfully", RepositoryName,
            methodName);

        var categories =
            (await connection.QueryAsync<Category>(
                sql: "",
                commandType: CommandType.Text,
                commandTimeout: 60))
            .ToList();

        _logger.LogInformation("{Repository}.{Method} - Execution completed successfully with output : {@WorkItems}",
            RepositoryName, methodName, categories);

        return categories;
    }

    public Task<List<Priority>> GetPriorities()
    {
        throw new NotImplementedException();
    }

    public Task<List<Status>> GetWorkItemStatus()
    {
        throw new NotImplementedException();
    }
}