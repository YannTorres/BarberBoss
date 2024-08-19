﻿using AutoMapper;
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
        CreateMap<RequestIncomeJson, Income>();
    }

    private void EntityToResponse()
    {
        CreateMap<Income, ResponseRegisteredIncomeJson>();
        CreateMap<Income, ResponseShortIncomeJson>();
        CreateMap<Income, ResponseExpenseJson>();
    }
}
