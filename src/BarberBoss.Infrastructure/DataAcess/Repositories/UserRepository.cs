using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAcess.Repositories;
internal class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly BarberBossDbContext _dbContext;
    public UserRepository(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistUserWithThisEmail(string email)
    {
        var existUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (existUser == null)
            return false;

        return true;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
         return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
    }
}
