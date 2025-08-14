using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Shouldly;
using TMS.Application.Features.Employees.Contracts.Create;
using TMS.Application.Features.Employees.ValidationHelpers;
using TMS.Application.Features.Employees.Validators;
using TMS.Application.Tests.Features.EmployeeTests.TestData;
using TMS.Core.Common;
using static TMS.Core.Common.Errors.ErrorMessages;

namespace TMS.Application.Tests.Features.EmployeeTests.ValidatorTests;

/// <summary>
/// 
/// </summary>
public class CreateEmployeeTypeRequestValidatorTests
{
    #region Fields
    
    /// <summary>
    /// 
    /// </summary>
    private readonly Mock<IEmployeeValidator> _mockEmployeeValidator = new();

    /// <summary>
    /// 
    /// </summary>
    private readonly CreateEmployeeTypeRequestValidator _validator;
    
    #endregion
    
    #region Constructor

    /// <summary>
    ///
    /// </summary>
    public CreateEmployeeTypeRequestValidatorTests()
    {
        _validator = new CreateEmployeeTypeRequestValidator(_mockEmployeeValidator.Object);
    }
    
    #endregion

    #region EmployeeType Name Validation

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_Type_Is_Null()
    {
        var employeeType = EmployeeTypeTestData.GetEmployeeTypeFaker().Generate();
        employeeType.Type = Constants.NullValue;

        var result = await _validator.TestValidateAsync(employeeType);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Type)
              .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeTypeRequest.Type)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_Type_Is_Empty()
    {
        var employeeType = EmployeeTypeTestData.GetEmployeeTypeFaker().Generate();
        employeeType.Type = string.Empty;

        var result = await _validator.TestValidateAsync(employeeType);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Type)
            .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeTypeRequest.Type)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_Type_Is_WhiteSpace()
    {
        var employeeType = EmployeeTypeTestData.GetEmployeeTypeFaker().Generate();
        employeeType.Type = " ";

        var result = await _validator.TestValidateAsync(employeeType);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Type)
            .WithErrorMessage(string.Format(ValidationMessages.CannotBeNullOrEmpty, nameof(CreateEmployeeTypeRequest.Type)));
    }

    [Fact]
    public async Task Should_Not_Throw_Error_When_Type_Is_Valid()
    {
        var employeeType = EmployeeTypeTestData.GetEmployeeTypeFaker().Generate();
        
        var result = await _validator.TestValidateAsync(employeeType);
        
        result.Errors.Count.ShouldBe(0);
        result.ShouldNotHaveValidationErrorFor(x => x.Type);
    }

    #endregion

    #region Existing EmployeeType Name Validation

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_New_Employee_Type_Exists()
    {
        var employeeType = EmployeeTypeTestData.GetEmployeeTypeFaker().Generate();

        _mockEmployeeValidator.Setup(x =>
                x.ValidateEmployeeTypeNameAsync(It.IsAny<string>(),
                    It.IsAny<ValidationContext<CreateEmployeeTypeRequest>>()))
            .Callback<string, ValidationContext<CreateEmployeeTypeRequest>>((_, context) =>
            {
                context.AddFailure(nameof(CreateEmployeeTypeRequest.Type),
                    string.Format(EmployeeTypeValidationMessages.EmployeeTypeFound, employeeType.Type));
            });
        
        var result = await _validator.TestValidateAsync(employeeType);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Type)
            .WithErrorMessage(string.Format(EmployeeTypeValidationMessages.EmployeeTypeFound, employeeType.Type));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Not_Throw_Error_When_New_Employee_Type_Does_Not_Exists()
    {
        var employeeType = EmployeeTypeTestData.GetEmployeeTypeFaker().Generate();

        _mockEmployeeValidator.Setup(x =>
            x.ValidateEmployeeTypeNameAsync(
                It.IsAny<string>(),
                It.IsAny<ValidationContext<CreateEmployeeTypeRequest>>()))
            .Returns(Task.CompletedTask);
        
        var result = await _validator.TestValidateAsync(employeeType);
        
        _mockEmployeeValidator.Verify(x => 
            x.ValidateEmployeeTypeNameAsync
                (It.IsAny<string>(), 
                    It.IsAny<ValidationContext<CreateEmployeeTypeRequest>>()), 
            Times.Once);
        
        result.Errors.Count.ShouldBe(0);
        result.ShouldNotHaveValidationErrorFor(x => x.Type);
    }

    #endregion
}