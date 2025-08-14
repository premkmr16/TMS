using Mapster;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Contracts.Get;
using TMS.Application.Features.Employees.Contracts.Update;
using TMS.Core.Entities;
namespace TMS.Application.Features.Employees.Mappers;

/// <summary>
/// 
/// </summary>
public class EmployeeMapper :  IRegister
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="config"></param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateEmployeeRequest, Employee>()
            .Map(dest => dest.Id, _ => Ulid.Empty);
        config.NewConfig<UpdateEmployeeRequest, Employee>();
        config.NewConfig<Employee, CreateEmployeeResponse>();
        config.NewConfig<Employee, UpdateEmployeeResponse>();
        
        config.NewConfig<CreateEmployeeTypeRequest, EmployeeType>()
            .Map(dest => dest.Id, _ => Ulid.Empty);
        config.NewConfig<CreateEmployeeTypeRequest, EmployeeTypeResponse>();
        
        config.NewConfig<ImportEmployeeRequest, Employee>()
            .Ignore(x => x.EmployeeType)
            .Map(dest => dest.Id, _ => Ulid.Empty);
    }
}