using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Incomes;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAcess.Repositories;
internal class IncomesRepository : IIncomeWriteOnlyRepository, IIncomeReadOnlyRepository, IIncomeUpdateOnlyRepository
{
    private readonly BarberBossDbContext _dbcontext;
    public IncomesRepository(BarberBossDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public async Task Add(Income income)
    {
        await _dbcontext.Incomes.AddAsync(income);
    }

    public async Task<bool> Delete(int Id)
    {
        var result = await _dbcontext.Incomes.FirstOrDefaultAsync(x => x.Id == Id);

        if (result == null)
            return false;

        _dbcontext.Incomes.Remove(result);

        return true;
    }

    public async Task<List<Income>> GetAll()
    {
        return await _dbcontext.Incomes.AsNoTracking().ToListAsync();
    }

    async Task<Income?> IIncomeReadOnlyRepository.GetById(int id)
    {
        return await _dbcontext.Incomes.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
    async Task<Income?> IIncomeUpdateOnlyRepository.GetById(int id)
    {
        return await _dbcontext.Incomes.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public void Update(Income income)
    {
        _dbcontext.Incomes.Update(income);
    }

    public async Task<List<Income>> FilterByWeek(DateOnly date)
    {
        /* var startDate = date;
        var endDate = date;

        switch (date.DayOfWeek)
        {
            case DayOfWeek.Sunday:
                startDate = date;
                endDate = date.AddDays(+6);
                break;
            case DayOfWeek.Monday:
                startDate = date.AddDays(-1);
                endDate = date.AddDays(+5);
                break;
            case DayOfWeek.Tuesday:
                startDate = date.AddDays(-2);
                endDate = date.AddDays(+4);
                break;
            case DayOfWeek.Wednesday:
                startDate = date.AddDays(-3);
                endDate = date.AddDays(+3);
                break;
            case DayOfWeek.Thursday:
                startDate = date.AddDays(-4);
                endDate = date.AddDays(+2);
                break;
            case DayOfWeek.Friday:
                startDate = date.AddDays(-5);
                endDate = date.AddDays(+1);
                break;
            case DayOfWeek.Saturday:
                startDate = date.AddDays(-6);
                endDate = date;
                break;
            default:
                startDate = date;
                endDate = date.AddDays(+6);
                break;
        } */

        var startDate = date.AddDays(-(int)date.DayOfWeek);
        var endDate = startDate.AddDays(6);

        var startDateTime = new DateTime(year: startDate.Year, month: startDate.Month, day: startDate.Day);
        var EndDateTime = new DateTime(year: endDate.Year, month: endDate.Month, day: endDate.Day, hour: 23, minute: 59, second: 59);

        return await _dbcontext.Incomes
            .AsNoTracking()
            .Where(d => d.Date >= startDateTime)
            .Where(d => d.Date <= EndDateTime)
            .OrderBy(d => d.Date)
            .ToListAsync();
    }
}
