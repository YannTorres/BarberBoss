using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Incomes;
using ClosedXML.Excel;

namespace BarberBoss.Application.UseCases.Income.Reports.Excel;
public class GenerateIncomesReportExcelUseCase : IGenerateIncomesReportExcelUseCase
{
    private readonly IIncomeReadOnlyRepository _repository;
    public GenerateIncomesReportExcelUseCase(IIncomeReadOnlyRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly date)
    {
        var incomes = await _repository.FilterByWeek(date);

        if (incomes.Count <= 0)
        {
            return [];
        }

        var workbook = new XLWorkbook();

        workbook.Style.Font.FontSize = 12;

        var worksheet = workbook.Worksheets.Add(date.ToString("Y"));

        InsertHeader(worksheet);

        InsertMainContent(worksheet, incomes);

        worksheet.Columns().Width = 25;

        var file = new MemoryStream();

        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Font.FontColor = XLColor.FromHtml("FFFFFF");
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");

        worksheet.Cells("A1:E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }

    private void InsertMainContent(IXLWorksheet worksheet, List<Domain.Entities.Income> incomes)
    {
        var raw = 2;
        foreach (var income in incomes)
        {
            worksheet.Cell($"A{raw}").Value = income.Title;
            worksheet.Cell($"B{raw}").Value = income.Date;
            worksheet.Cell($"C{raw}").Value = income.PaymentType.PaymentTypeToString();
            worksheet.Cell($"D{raw}").Value = income.Amount;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"-R$ #,##0.00";
            worksheet.Cell($"E{raw}").Value = income.Description;

            raw += 1;
        }
    }
}
