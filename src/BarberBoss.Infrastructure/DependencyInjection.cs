using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Incomes;
using BarberBoss.Domain.Repositories.User;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Infrastructure.DataAcess;
using BarberBoss.Infrastructure.DataAcess.Repositories;
using BarberBoss.Infrastructure.Security.Tokens;
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
        AddToken(serviceProvider, configuration);

        serviceProvider.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
    }
    private static void AddToken(IServiceCollection service, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        service.AddScoped<IAcessTokenGenerator>(config => new JwtTokenGenerator(signingKey!, expirationTimeMinutes));
    }

    private static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();

        // Incomes Repository
        service.AddScoped<IIncomeWriteOnlyRepository, IncomesRepository>();
        service.AddScoped<IIncomeReadOnlyRepository, IncomesRepository>();
        service.AddScoped<IIncomeUpdateOnlyRepository, IncomesRepository>();

        // User Repository
        service.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        service.AddScoped<IUserReadOnlyRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection service, IConfiguration configuration)
    {
        var conectionString = configuration.GetConnectionString("DefaultConnection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));
        service.AddDbContext<BarberBossDbContext>(config => config.UseMySql(conectionString, serverVersion));
    }
}
