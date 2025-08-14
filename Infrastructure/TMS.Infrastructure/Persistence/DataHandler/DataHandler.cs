using Microsoft.Extensions.Logging;
using TMS.Application.Cache;
using TMS.Application.Common.Models;
using TMS.Application.DataHandler;

namespace TMS.Infrastructure.Persistence.DataHandler;

/// <summary>
/// Provides methods to handle data retrieval and caching operations.
/// It implements the <see cref="IDataHandler"/> interface and uses an <see cref="ICacheService"/> for caching purposes.
/// The class includes methods to get or load data with pagination support
/// </summary>
public class DataHandler : IDataHandler
{
    /// <summary>
    /// 
    /// </summary>
    private const string HandlerName = nameof(DataHandler);
    
    /// <summary>
    /// 
    /// </summary>
    private readonly ICacheService _cacheService;
    
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<DataHandler> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cacheService"></param>
    /// <param name="logger"></param>
    public DataHandler(ICacheService cacheService, ILogger<DataHandler> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    /// <inheritdoc cref="GetOrLoadAsync{T}(string, PaginationRequest, System.Func{System.Threading.Tasks.Task{PaginatedResponse{T}}})"/>
    public async Task<PaginatedResponse<T>> GetOrLoadAsync<T>(
        string key, 
        PaginationRequest paginationRequest, 
        Func<Task<PaginatedResponse<T>>> loadPredicate) where T : class
    {
        const string methodName = nameof(GetOrLoadAsync);
        
        _logger.LogInformation("{HandlerName}.{MethodName} - Execution started successfully with input : {CacheKey}",
            HandlerName, methodName, key);
        
        var cacheKey = paginationRequest.FetchWithPagination 
            ? $"{key}_{paginationRequest.PageNumber}_{paginationRequest.PageSize}" 
            : key;
        
        var cachedData = _cacheService.Get<PaginatedResponse<T>>(cacheKey);

        if (cachedData is { Data.Count: > 0 }) 
            return cachedData;

        var data = await loadPredicate.Invoke();

        if (data is { Data.Count: > 0 }) 
            _cacheService.Set(cacheKey, data, TimeSpan.FromMinutes(5));
        
        _logger.LogInformation("{HandlerName}.{MethodName} - Execution completed successfully with output : {@CachedData}",
            HandlerName, methodName, data);

        return data;
    }
    
    /// <inheritdoc cref="GetOrLoadAsync{T}(string, System.Func{System.Threading.Tasks.Task{System.Collections.Generic.List{T}}})"/>
    public async Task<List<T>> GetOrLoadAsync<T>(string key, Func<Task<List<T>>> loadPredicate) where T : class
    {
        const string methodName = nameof(GetOrLoadAsync);
        
        _logger.LogInformation("{HandlerName}.{MethodName} - Execution started successfully with input : {CacheKey}",
            HandlerName, methodName, key);
        
        var cachedData = _cacheService.GetAll<T>(key);

        if (cachedData.Count > 0) 
            return cachedData;

        var data = await loadPredicate.Invoke();

        if (data.Count > 0) 
            _cacheService.Set(key, data, TimeSpan.FromMinutes(5));
        
        _logger.LogInformation("{HandlerName}.{MethodName} - Execution completed successfully with output : {@CachedData}",
            HandlerName, methodName, data);

        return data;
    }
    
    /// <inheritdoc cref="GetOrLoadAsync{T}(string, System.Func{System.Threading.Tasks.Task{T}})"/>
    public async Task<T> GetOrLoadAsync<T>(string key, Func<Task<T>> loadPredicate) where T : class
    {
        const string methodName = nameof(GetOrLoadAsync);
        
        _logger.LogInformation("{HandlerName}.{MethodName} - Execution started successfully with input : {CacheKey}",
            HandlerName, methodName, key);
        
        var cachedData = _cacheService.Get<T>($"{typeof(T).FullName}_{key}");

        if (cachedData is not null) 
            return cachedData;

        var data = await loadPredicate.Invoke();

        if (data is not null) 
            _cacheService.Set(key, data, TimeSpan.FromMinutes(5));
        
        _logger.LogInformation("{HandlerName}.{MethodName} - Execution completed successfully with output : {@CachedData}",
            HandlerName, methodName, data);

        return data;
    }
}