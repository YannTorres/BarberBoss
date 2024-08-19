using AutoMapper;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Repositories.Incomes;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Income.GetById;
public class GetIncomeByIdUseCase : IGetIncomeByIdUseCase
{
    private readonly IIncomeReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public GetIncomeByIdUseCase(IIncomeReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseExpenseJson> Execute(int id)
    {
        var result = await _repository.GetById(id);

        if (result == null)
            throw new NotFoundException(ResourceErrorMessages.INCOME_NOT_FOUND);  

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
