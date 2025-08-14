namespace TMS.Application.Cache;

/// <summary>
/// 
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    List<T> GetAll<T>(string key);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T Get<T>(string key);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiration"></param>
    /// <typeparam name="T"></typeparam>
    void Set<T>(string key, T value, TimeSpan expiration);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    void Remove(string key);
}