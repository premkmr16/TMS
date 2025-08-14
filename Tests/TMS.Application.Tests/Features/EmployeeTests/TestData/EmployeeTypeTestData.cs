using Bogus;
using TMS.Application.Features.Employees.Contracts.Create;

namespace TMS.Application.Tests.Features.EmployeeTests.TestData;

/// <summary>
/// Test Data for Employee Type related features.
/// This class provides methods to generate fake data for CreateEmployeeTypeRequest.
/// It uses the Bogus library to create realistic test data for unit tests.
/// </summary>
public static class EmployeeTypeTestData
{
    /// <summary>
    /// Employee Types used in the application.
    /// This list contains various employee types that can be used for testing purposes.
    /// </summary>
    private static readonly List<string> EmployeeTypes = ["Contractor", "Intern", "Software Engineer", "Senior Software Engineer"];
    
    /// <summary>
    /// Creates a Faker instance for CreateEmployeeTypeRequest.
    /// This method generates fake data for creating a new employee type.
    /// </summary>
    /// <returns></returns>
    public static Faker<CreateEmployeeTypeRequest> GetEmployeeTypeFaker()
    {
        return new Faker<CreateEmployeeTypeRequest>()
            .RuleFor(x => x.Type, f => f.PickRandom(EmployeeTypes));
    }
}