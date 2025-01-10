using AutoMapper;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Repositories.Incomes;
using BarberBoss.Domain.Services.LoggedUser;

namespace BarberBoss.Application.UseCases.Income.GetAll;
public class GetAllIncomesUseCase : IGetAllIncomesUseCase
{
    private readonly IIncomeReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public GetAllIncomesUseCase(IIncomeReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetAll();    

        return new ResponseExpensesJson
        {
            Incomes = _mapper.Map<List<ResponseShortIncomeJson>>(result)
        };
    }
}
