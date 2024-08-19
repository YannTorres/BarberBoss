using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Incomes;
using BarberBoss.Infrastructure.DataAcess;
using BarberBoss.Infrastructure.DataAcess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure;
public static class DependencyInjection
{
    public static void AddInfraestructure(this IServiceCollection serviceProvider, IConfiguration configuration)
    {
        AddRepositories(serviceProvider);
        AddDbContext(serviceProvider, configuration);
    }

    private static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IIncomeWriteOnlyRepository, IncomesRepository>();
        service.AddScoped<IIncomeReadOnlyRepository, IncomesRepository>();
        service.AddScoped<IIncomeUpdateOnlyRepository, IncomesRepository>();
    }

    private static void AddDbContext(IServiceCollection service, IConfiguration configuration)
    {
        var conectionString = configuration.GetConnectionString("DefaultConnection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));
        service.AddDbContext<BarberBossDbContext>(config => config.UseMySql(conectionString, serverVersion));
    }
}
