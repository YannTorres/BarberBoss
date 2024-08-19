using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Incomes;
using AutoMapper;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Income.Register;
public class RegisterIncomeUseCase : IRegisterIncomeUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIncomeWriteOnlyRepository _repository;
    private readonly IMapper _mapper;
    public RegisterIncomeUseCase(IUnitOfWork unitOfWork, IIncomeWriteOnlyRepository repository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredIncomeJson> Execute(RequestIncomeJson request)
    {
        Validator(request);

        var entity = _mapper.Map<Domain.Entities.Income>(request);

        await _repository.Add(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredIncomeJson>(entity);

    }

    public void Validator(RequestIncomeJson request)
    {
        var validator = new IncomeValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false) 
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
