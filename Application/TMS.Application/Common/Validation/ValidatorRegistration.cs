using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TMS.Application.Common.Validation;

/// <summary>
/// 
/// </summary>
public static class ValidatorRegistration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}