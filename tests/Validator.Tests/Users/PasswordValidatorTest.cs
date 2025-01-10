using BarberBoss.Application.UseCases.Users;
using BarberBoss.Communication.Requests;
using FluentAssertions;
using FluentValidation;

namespace Validator.Tests.Users;
public class PasswordValidatorTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("           ")]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("AaAaAaAa")]
    [InlineData("AaAaAaAa1")]
    public void Error_Password_Invalid(string password)
    {
        var validator = new PasswordValidator<RequestRegisterUserJson>();

        var result = validator.IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);

        result.Should().BeFalse();
    }
}
