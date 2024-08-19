using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Response;
public class ResponseExpenseJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public DateTime Date { get; set; }
}
