using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TMS.Application.Common.Validation;

public static class ValidatorRegistration
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}