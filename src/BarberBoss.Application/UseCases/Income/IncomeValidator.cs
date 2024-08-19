using BarberBoss.Communication.Requests;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Income;
public class IncomeValidator : AbstractValidator<RequestIncomeJson>
{
    public IncomeValidator()
    {
        RuleFor(income => income.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
        RuleFor(income => income.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.INCOME_CANNOT_BE_FOR_THE_FUTURE);
        RuleFor(income => income.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_REQUIRED);
        RuleFor(income => income.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
    }
}
