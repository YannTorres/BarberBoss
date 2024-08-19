using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Reports;

namespace BarberBoss.Domain.Extensions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {

        return paymentType switch
        {
            PaymentType.Debit => ResourceReportGenerationMessages.DEBIT_CARD,
            PaymentType.Credit => ResourceReportGenerationMessages.CREDIT_CARD,
            PaymentType.Cash => ResourceReportGenerationMessages.CASH,
            PaymentType.Pix => ResourceReportGenerationMessages.PIX,
            _ => string.Empty
        };
    
    }
}
