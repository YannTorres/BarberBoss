using AutoMapper;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Repositories.Incomes;

namespace BarberBoss.Application.UseCases.Income.GetAll;
public class GetAllIncomesUseCase : IGetAllIncomesUseCase
{
    private readonly IIncomeReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public GetAllIncomesUseCase(IIncomeReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseExpensesJson
        {
            Incomes = _mapper.Map<List<ResponseShortIncomeJson>>(result)
        };
    }
}
