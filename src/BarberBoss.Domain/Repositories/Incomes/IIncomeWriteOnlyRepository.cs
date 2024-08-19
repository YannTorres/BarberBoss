using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Incomes;

public interface IIncomeWriteOnlyRepository
{
    /// <summary>
    /// Function to add a new Income to the database
    /// </summary>
    /// <param name="income"></param>
    /// <returns></returns>
    Task Add(Income income);

    /// <summary>
    /// Function to delete an Income from database. Returns True if the process of removing an Income was a sucess.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(int id);
}
