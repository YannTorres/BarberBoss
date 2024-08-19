namespace BarberBoss.Application.UseCases.Income.Reports.PDF;
public interface IGenerateIncomesReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly dateOnly);
}
