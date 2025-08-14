using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TMS.Application.Cache;

namespace TMS.Infrastructure.Persistence.Cache;

/// <summary>
/// Implements methods for storing and retrieval of cached data.
/// This class uses an in-memory cache to store and retrieve data efficiently.
/// It provides methods to get, set, and remove cached items, as well as to retrieve
/// </summary>
public class CacheService : ICacheService
{
    /// <summary>
    /// The name of the service used for logging.
    /// </summary>
    private const string ServiceName = nameof(CacheService);
    
    /// <summary>
    /// The in-memory cache instance used for storing cached data.
    /// </summary>
    private readonly IMemoryCache _memoryCache;
    
    /// <summary>
    /// Logger instance for capturing cache service logs.
    /// </summary>
    private readonly ILogger<CacheService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheService"/> class.
    /// </summary>
    /// <param name="memoryCache">Defines the local in-memory cache <see cref="IMemoryCache"/>.</param>
    /// <param name="logger">Defines the logger instance <see cref="ILogger{CacheService}"/>.</param>
    public CacheService(IMemoryCache memoryCache, ILogger<CacheService> logger)
    {
        _memoryCache = memoryCache;
        _logger = logger;
    }
    
    /// <inheritdoc cref="ICacheService.GetAll{T}"/>
    public List<T> GetAll<T>(string key)
    {
        const string methodName = nameof(GetAll);

        _logger.LogInformation("[{Service}][{Method}] - Execution started successfully with input : {key}",
            ServiceName, methodName, key);
        
        var cachedData = _memoryCache.TryGetValue(key, out List<T> data) ? data : [];
        
        _logger.LogInformation("[{Service}][{Method}] - Execution completed successfully with output : {@Employee}", 
            ServiceName, methodName, cachedData);
        
        return cachedData;
    }

    /// <inheritdoc cref="ICacheService.Get{T}"/>
    public T Get<T>(string key)
    {
        const string methodName = nameof(Get);

        _logger.LogInformation("[{Service}][{Method}] - Execution started successfully with input : {key}",
            ServiceName, methodName, key);

        var cachedData = _memoryCache.Get<T>(key);

        _logger.LogInformation("[{Service}][{Method}] - Execution completed successfully with output : {@Employee}",
            ServiceName, methodName, cachedData);

        return cachedData;
    }

    /// <inheritdoc cref="ICacheService.Set{T}"/>
    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        const string methodName = nameof(Set);
        
        _logger.LogInformation("[{Service}][{Method}] - Execution started successfully with input : {key}, {@Value}",
            ServiceName, methodName, key, value);

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(expiration)
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(20))
            .SetPriority(CacheItemPriority.Normal)
            .RegisterPostEvictionCallback((keyObj, valueObj, reason, _) =>
            {
                _logger.LogInformation("[{Service}][{Method}] - Cache entry evicted for key: {Key} and {@Value}, reason: {Reason}",
                    ServiceName, methodName, keyObj, valueObj, reason);
            });
        
        _memoryCache.Set(key, value, cacheOptions);
        
        _logger.LogInformation("[{Service}][{Method}] - Execution completed successfully with output : {@Value}",
            ServiceName, methodName, value);
    }

    /// <inheritdoc cref="ICacheService.Remove"/>
    public void Remove(string key)
    {   
        const string methodName = nameof(Remove);
        
        _logger.LogInformation("[{Service}][{Method}] - Execution started successfully with input : {key}",
            ServiceName, methodName, key);
        
        _memoryCache.Remove(key);
        
        _logger.LogInformation("[{Service}][{Method}] - Execution completed successfully",
            ServiceName, methodName);
    }
}