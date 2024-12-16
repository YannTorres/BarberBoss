using BarberBoss.Infrastructure.DataAcess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure.Migrations;
public class DataBaseMigration
{
    public static async Task MigrateDataBase(IServiceProvider serviceProvider)
    {
        var dbcontext = serviceProvider.GetRequiredService<BarberBossDbContext>();

        await dbcontext.Database.MigrateAsync();
    }
}
