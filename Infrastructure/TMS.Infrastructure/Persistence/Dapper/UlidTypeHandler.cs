using System.Data;
using Dapper;

namespace TMS.Infrastructure.Persistence.Dapper;

/// <summary>
/// 
/// </summary>
public class UlidTypeHandler : SqlMapper.TypeHandler<Ulid>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    public override void SetValue(IDbDataParameter parameter, Ulid value)
    {
       parameter.Value = value.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override Ulid Parse(object value)
    {
        return Ulid.Parse(value.ToString());
    }
}