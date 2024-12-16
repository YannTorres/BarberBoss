using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        // Income
        CreateMap<RequestRegisterIncomeJson, Income>();
        // User
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());
    }

    private void EntityToResponse()
    {
        // Income
        CreateMap<Income, ResponseRegisteredIncomeJson>();
        CreateMap<Income, ResponseShortIncomeJson>();
        CreateMap<Income, ResponseExpenseJson>();

        // User
        CreateMap<User, ResponseRegisteredUserJson>();
    }
}
