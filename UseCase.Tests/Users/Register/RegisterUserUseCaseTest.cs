using BarberBoss.Application.UseCases.Users.Register;
using BarberBoss.Exception.ExceptionBase;
using BarberBoss.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Security.Cryptography;
using CommonTestUtilities.Security.Token;

namespace UseCase.Tests.Users.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
    }

    //[Fact]
    //public async Task Error_Name_Empty()
    //{
    //    var request = RequestRegisterUserJsonBuilder.Build();
    //    request.Name = string.Empty;

    //    var useCase = CreateUseCase();
    //    var act = async () => await useCase.Execute(request); // Estamos armazenando uma função nessa variável

    //    var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

    //    result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_EMPTY));
    //}

    //[Fact]
    //public async Task Error_Email_Already_Exists()
    //{
    //    var request = RequestRegisterUserJsonBuilder.Build();

    //    var useCase = CreateUseCase(request.Email);
    //    var act = async () => await useCase.Execute(request); // Estamos armazenando uma função nessa variável

    //    var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

    //    result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
    //}

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var acessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var readOnlyRepository = new UserReadOnlyRepositoryBuilder().Build();

        return new RegisterUserUseCase(unitOfWork, writeOnlyRepository, 
            readOnlyRepository, mapper, passwordEncripter, acessTokenGenerator);
    }
}
