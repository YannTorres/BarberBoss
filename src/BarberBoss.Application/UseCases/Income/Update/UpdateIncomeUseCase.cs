using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Incomes;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Income.Update;
public class UpdateIncomeUseCase : IUpdateIncomeUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIncomeUpdateOnlyRepository _repository;
    private readonly IMapper _mapper;
    public UpdateIncomeUseCase(IIncomeUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }
    public async Task Execute(int id, RequestIncomeJson request)
    {
        Validator(request);
        
        var expense = await _repository.GetById(id);

        if (expense == null)
            throw new NotFoundException(ResourceErrorMessages.INCOME_NOT_FOUND);

        _mapper.Map(request, expense);

        _repository.Update(expense);

        await _unitOfWork.Commit();
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
