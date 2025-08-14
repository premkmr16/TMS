using TMS.Application.Common.Models;

namespace TMS.Application.DataHandler;

/// <summary>
/// 
/// </summary>
public interface IDataHandler
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="paginationRequest"></param>
    /// <param name="loadPredicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<PaginatedResponse<T>> GetOrLoadAsync<T>(string key, PaginationRequest paginationRequest, Func<Task<PaginatedResponse<T>>> loadPredicate)
        where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="loadPredicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<List<T>> GetOrLoadAsync<T>(string key, Func<Task<List<T>>> loadPredicate) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="loadPredicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T> GetOrLoadAsync<T>(string key, Func<Task<T>> loadPredicate) where T : class;
}