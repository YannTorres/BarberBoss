using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;
public static class RequestRegisterIncomeJsonBuilder
{
    public static RequestIncomeJson Build()
    {
        var faker = new Faker();

        return new Faker<RequestIncomeJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker.Random.Enum<PaymentType>())
            .RuleFor(r => r.Amount, faker.Random.Double(min: 0, max: 10000000000));
    } 
}
