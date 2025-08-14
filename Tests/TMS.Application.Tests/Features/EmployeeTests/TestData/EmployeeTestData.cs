using Bogus;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.Contracts.Update;
using TMS.Core.Entities;

namespace TMS.Application.Tests.Features.EmployeeTests.TestData;

/// <summary>
/// Test data for Employee related features.
/// This class provides methods to generate fake data for Employee, CreateEmployeeRequest, and UpdateEmployeeRequest.
/// It uses the Bogus library to create realistic test data for unit tests.
/// </summary>
public static class EmployeeTestData
{
    /// <summary>
    /// Creates a Faker instance for Employee entity.
    /// This method generates fake data for new employee.
    /// </summary>
    /// <returns>The <see cref="Faker{Employee}"/></returns>
    public static Faker<Employee> GetEmployeeFaker()
    {
        return new Faker<Employee>()
            .RuleFor(x => x.Id, _ => Ulid.NewUlid().ToString())
            .RuleFor(x => x.EmployeeNumber, f => $"EMP{f.Random.Number(1000, 10000)}")
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber("##########"))
            .RuleFor(x => x.DateOfBirth, f => f.Date.Between(DateTime.Today.AddYears(-30), DateTime.Today.AddYears(-18)))
            .RuleFor(x => x.EmployeeTypeId, _ => Ulid.NewUlid().ToString())
            .RuleFor(x => x.IsActive, _ => true)
            .RuleFor(x => x.StartDate, _ => DateTimeOffset.UtcNow);
    }

    /// <summary>
    /// Creates a Faker instance for CreateEmployeeRequest.
    /// This method generates fake data for creating a new employee.
    /// </summary>
    /// <returns>The <see cref="Faker{CreateEmployeeRequest}"/></returns>
    public static Faker<CreateEmployeeRequest> GetCreateEmployeeRequestFaker()
    {
        return new Faker<CreateEmployeeRequest>()
            .RuleFor(x => x.EmployeeNumber, f => $"EMP{f.Random.Number(1000, 10000)}")
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber("##########"))
            .RuleFor(x => x.DateOfBirth, f => f.Date.Between(DateTime.Today.AddYears(-30), DateTime.Today.AddYears(-18)))
            .RuleFor(x => x.EmployeeTypeId, _ => Ulid.NewUlid().ToString())
            .RuleFor(x => x.IsVerified, f => false)
            .RuleFor(x => x.StartDate, _ => DateTimeOffset.Now);
    }

    /// <summary>
    /// Creates a Faker instance for UpdateEmployeeRequest.
    /// This method generates fake data for updating an existing employee.
    /// </summary>
    /// <returns>The <see cref="Faker{UpdateEmployeeRequest}"/></returns>
    public static Faker<UpdateEmployeeRequest> GetUpdateEmployeeRequestFaker()
    {
        return new Faker<UpdateEmployeeRequest>()
            .RuleFor(x => x.Id, _ => Ulid.NewUlid().ToString())
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber("##########"))
            .RuleFor(x => x.DateOfBirth, f => f.Date.Between(DateTime.Today.AddYears(-30), DateTime.Today.AddYears(-18)))
            .RuleFor(x => x.EmployeeTypeId, _ => Ulid.NewUlid().ToString())
            .RuleFor(x => x.StartDate, _ => DateTimeOffset.Now);
    }
}