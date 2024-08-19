using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Incomes;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Income.Delete;
public class DeleteIncomeUseCase : IDeleteIncomeUseCase
{
    private readonly IIncomeWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteIncomeUseCase(IIncomeWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(int Id)
    {
        var result = await _repository.Delete(Id);

        if (result == false)
            throw new NotFoundException(ResourceErrorMessages.INCOME_NOT_FOUND);

        await _unitOfWork.Commit();
    }
}
