using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Shouldly;
using TMS.Application.Features.Employees.Contracts.Update;
using TMS.Application.Features.Employees.ValidationHelpers;
using TMS.Application.Features.Employees.Validators;
using TMS.Application.Tests.Features.EmployeeTests.TestData;
using TMS.Core.Common;
using TMS.Core.Common.Errors;

namespace TMS.Application.Tests.Features.EmployeeTests.ValidatorTests;

public class UpdateEmployeeRequestValidatorTests
{
    #region Fields
    
    private readonly Mock<IEmployeeValidator> _mockEmployeeValidator = new();

    private readonly UpdateEmployeeRequestValidator _validator;
    
    #endregion

    #region Constructor

    public UpdateEmployeeRequestValidatorTests()
    {
        _validator = new UpdateEmployeeRequestValidator(_mockEmployeeValidator.Object);
    }
    
    #endregion

    #region EmployeeId Validation

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeId_Is_Empty()
    {
        var request = EmployeeTestData.GetUpdateEmployeeRequestFaker().Generate();
        request.Id = string.Empty;
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Id)
              .WithErrorMessage(string.Format(ErrorMessages.ValidationMessages.CannotBeNullOrEmpty, nameof(UpdateEmployeeRequest.Id)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeId_Is_Null()
    {
        var request = EmployeeTestData.GetUpdateEmployeeRequestFaker().Generate();
        request.Id = Constants.NullValue;
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Id)
              .WithErrorMessage(string.Format(ErrorMessages.ValidationMessages.CannotBeNullOrEmpty, nameof(UpdateEmployeeRequest.Id)));
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeId_Is_WhiteSpace()
    {
        var request = EmployeeTestData.GetUpdateEmployeeRequestFaker().Generate();
        request.Id = " ";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage(string.Format(ErrorMessages.ValidationMessages.CannotBeNullOrEmpty, nameof(UpdateEmployeeRequest.Id)));
    }

    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeId_Length_Is_Does_Not_Match_Criteria()
    {
        var request = EmployeeTestData.GetUpdateEmployeeRequestFaker().Generate();
        request.Id = "01JXS9B03QPB2AQKVJVMXVA9VW8";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage(string.Format(ErrorMessages.ValidationMessages.InvalidStringLength, "Ulid"));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EmployeeId_Is_In_Invalid_Format()
    {
        var request = EmployeeTestData.GetUpdateEmployeeRequestFaker().Generate();
        request.Id = "01JX&9B_3QPB2AQKVJVMXVA9VW";
        
        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage(string.Format(ErrorMessages.ValidationMessages.InvalidStringFormat, "Ulid", request.Id));
    }

    #endregion

    #region Employee EndDate Validation
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task Should_Throw_Error_When_EndDate_Is_Provided_For_EmployeeTypes_Other_Than_Contractor_Or_Intern()
    {
        var request = EmployeeTestData.GetUpdateEmployeeRequestFaker().Generate();

        _mockEmployeeValidator.Setup(x =>
                x.ValidateEmployeeEndDate(
                    It.IsAny<string>(),
                    It.IsAny<DateTimeOffset>(),
                    It.IsAny<DateTimeOffset>(),
                    It.IsAny<ValidationContext<UpdateEmployeeRequest>>()))
            .Callback<string, DateTimeOffset, DateTimeOffset,
                ValidationContext<UpdateEmployeeRequest>>((_, _, _, context) =>
            {
                context.AddFailure(nameof(UpdateEmployeeRequest.EndDate),
                    ErrorMessages.EmployeeValidationMessages.EndDateShouldBeNull);
            });

        var result = await _validator.TestValidateAsync(request);
        
        result.Errors.Count.ShouldBe(1);
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage(ErrorMessages.EmployeeValidationMessages.EndDateShouldBeNull);
    }

    #endregion
}