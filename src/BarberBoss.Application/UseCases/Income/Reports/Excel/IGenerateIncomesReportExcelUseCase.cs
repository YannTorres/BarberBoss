namespace BarberBoss.Application.UseCases.Income.Reports.Excel;
public interface IGenerateIncomesReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly date);
}
