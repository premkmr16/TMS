using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TMS.Application.ConnectionFactory;

namespace TMS.Infrastructure.ConnectionFactory;

/// <summary>
/// 
/// </summary>
public sealed class ConnectionFactory : IConnectionFactory
{
    /// <summary>
    /// 
    /// </summary>
    private readonly string _connectionString;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public ConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}