using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Requests;
public class RequestRegisterIncomeJson
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public DateTime Date { get; set; }
}
