using System.Data;

namespace TMS.Application.ConnectionFactory;

/// <summary>
/// 
/// </summary>
public interface IConnectionFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IDbConnection CreateConnection();
}