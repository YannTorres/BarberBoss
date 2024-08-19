
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Incomes;
using BarberBoss.Exception;
using MigraDoc.DocumentObjectModel;

namespace BarberBoss.Application.UseCases.Income.Reports.PDF;
public class GenerateIncomesReportPdfUseCase : IGenerateIncomesReportPdfUseCase
{
    private readonly IIncomeReadOnlyRepository _repository;
    public GenerateIncomesReportPdfUseCase(IIncomeReadOnlyRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly dateOnly)
    {
        var incomes = await _repository.FilterByWeek(dateOnly);

        if (incomes.Count <= 0 )
        {
            return [];
        }

        var document = CreateDocument();
    }

    private Document CreateDocument()
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessages.TOTAL_EARNINGS_IN_WEEK}";
        document.Info.Author = "Yann Torres";

        // Falta coisa aqui

        return document;
    }
}
