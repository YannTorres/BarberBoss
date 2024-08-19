using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.Income.Delete;
using BarberBoss.Application.UseCases.Income.GetAll;
using BarberBoss.Application.UseCases.Income.GetById;
using BarberBoss.Application.UseCases.Income.Register;
using BarberBoss.Application.UseCases.Income.Reports.Excel;
using BarberBoss.Application.UseCases.Income.Update;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;
public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services) 
    {
        services.AddScoped<IRegisterIncomeUseCase, RegisterIncomeUseCase>();
        services.AddScoped<IGetIncomeByIdUseCase, GetIncomeByIdUseCase>();
        services.AddScoped<IGetAllIncomesUseCase, GetAllIncomesUseCase>();
        services.AddScoped<IDeleteIncomeUseCase, DeleteIncomeUseCase>();
        services.AddScoped<IUpdateIncomeUseCase, UpdateIncomeUseCase>();
        services.AddScoped<IGenerateIncomesReportExcelUseCase, GenerateIncomesReportExcelUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
}
