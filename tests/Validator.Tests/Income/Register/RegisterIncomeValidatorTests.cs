using BarberBoss.Application.UseCases.Income;
using BarberBoss.Communication.Enums;
using BarberBoss.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validator.Tests.Income.Register;
public class RegisterIncomeValidatorTests
{
    [Fact]
    public void Sucess()
    {
        //Arrenge => É onde instanciamos o que precisamos para o nosso teste (Validator e a Request nesse caso)
        var validator = new IncomeValidator();
        var fakeRequest = RequestRegisterIncomeJsonBuilder.Build();

        //Act => É a ação que precisamos (Validar a requisição)
        var result = validator.Validate(fakeRequest);

        //Assert => É o que esperamos do nosso Validator
        result.IsValid.Should().BeTrue();
    }
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("        ")]
    public void TitleEmptyError(string title)
    {
        var validator = new IncomeValidator();
        var fakeRequest = RequestRegisterIncomeJsonBuilder.Build();
        fakeRequest.Title = title;

        var result = validator.Validate(fakeRequest);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10.29)]
    public void AmountError(double amount)
    {
        var validator = new IncomeValidator();
        var fakeRequest = RequestRegisterIncomeJsonBuilder.Build();
        fakeRequest.Amount = amount;

        var result = validator.Validate(fakeRequest);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }

    [Fact]
    public void PaymentTypeError()
    {
        var validator = new IncomeValidator();
        var fakeRequest = RequestRegisterIncomeJsonBuilder.Build();
        fakeRequest.PaymentType = (PaymentType)5;

        var result = validator.Validate(fakeRequest);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_REQUIRED));
    }

    [Fact]
    public void DateForFutureError()
    {
        var validator = new IncomeValidator();
        var fakeRequest = RequestRegisterIncomeJsonBuilder.Build();
        fakeRequest.Date = DateTime.Now.AddDays(+20);

        var result = validator.Validate(fakeRequest);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INCOME_CANNOT_BE_FOR_THE_FUTURE));
    }
}
