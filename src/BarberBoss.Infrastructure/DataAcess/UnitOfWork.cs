using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.DataAcess;

namespace BarberBoss.Infrastructure.DataAcess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly BarberBossDbContext _dbContext;
    public UnitOfWork(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
