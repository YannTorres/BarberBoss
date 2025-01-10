using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.User;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserWriteOnlyRepository _repositoryWriteOnly;
    private readonly IUserReadOnlyRepository _repositoryReadOnly;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAcessTokenGenerator _acessTokenGenerator;
    public RegisterUserUseCase(
        IUnitOfWork unitOfWork, 
        IUserWriteOnlyRepository repositoryWriteOnly,
        IUserReadOnlyRepository repositoryReadOnly, 
        IMapper mapper, 
        IPasswordEncripter passwordEncripter,
        IAcessTokenGenerator acessTokenGenerator)
    {
        _repositoryWriteOnly = repositoryWriteOnly;
        _repositoryReadOnly = repositoryReadOnly;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _acessTokenGenerator = acessTokenGenerator;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await ValidatorAsync(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encript(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _repositoryWriteOnly.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson()
        {
            Name = user.Name,
            Token = _acessTokenGenerator.GenerateToken(user)
        };
    }

    private async Task ValidatorAsync(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var existUserWithThisEmail = await _repositoryReadOnly.ExistUserWithThisEmail(request.Email);

        if (existUserWithThisEmail == true || !result.IsValid)
        {
            if (existUserWithThisEmail == true)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREDY_EXIST));

            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
