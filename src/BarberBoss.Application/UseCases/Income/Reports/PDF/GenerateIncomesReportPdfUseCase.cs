
using BarberBoss.Application.UseCases.Income.Reports.PDF.Colors;
using BarberBoss.Application.UseCases.Income.Reports.PDF.Fonts;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Incomes;
using BarberBoss.Exception;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using System.Globalization;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Income.Reports.PDF;
public class GenerateIncomesReportPdfUseCase : IGenerateIncomesReportPdfUseCase
{
    private const int HEIGHT_ROW = 25;
    private readonly IIncomeReadOnlyRepository _repository;
    public GenerateIncomesReportPdfUseCase(IIncomeReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new IncomesReportFontResolver();
    }
    public async Task<byte[]> Execute(DateOnly dateOnly)
    {
        var incomes = await _repository.FilterByWeek(dateOnly);

        if (incomes.Count <= 0 )
        {
            return [];
        }

        var document = CreateDocument();
        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(page);
        var totalIncomesValue = incomes.Sum(e => e.Amount);
        CreateTotalIncomesSection(page, dateOnly, totalIncomesValue);

        foreach (var income in incomes)
        {
            var table = CreateIncomeTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW;

            AddIncomeTitle(row.Cells[0], income.Title);
            AddIncomeTitle(row.Cells[3]);

            row = table.AddRow();
            row.Height = HEIGHT_ROW;

            var incomeDate = income.Date.ToString("dddd, dd/MM/yyyy");
            var FirstCharUpperExpenseDate = string.Concat(incomeDate.First().ToString().ToUpper(), incomeDate.AsSpan(1));

            row.Cells[0].AddParagraph(FirstCharUpperExpenseDate);
            StyleForIncomeInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[1].AddParagraph(income.Date.ToString("hh:mm tt"));
            StyleForIncomeInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(income.PaymentType.ToString()/*.PaymentTypeToString()*/);
            StyleForIncomeInformation(row.Cells[2]);

            AddAmountForIncome(row.Cells[3], income.Amount);

            if (!string.IsNullOrEmpty(income.Description))
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW;

                descriptionRow.Cells[0].AddParagraph(income.Description);
                descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 9, Color = ColorsHelper.GRAY };
                descriptionRow.Cells[0].Shading.Color = ColorsHelper.MEGAULTRALIGHT_GREEN;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = 20;

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);
        }


        return RenderDocument(document);
    }

    private static Document CreateDocument()
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessages.WEEKLY_REVENUE}";
        document.Info.Author = "Yann Torres";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.ROBOTO_REGULAR;

        return document;
    }

    private static Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 40;
        section.PageSetup.BottomMargin = 60;

        return section;
    }

    private static void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location); // Ir nas propriedades da imagem e marcar copiar sempre para o diretório de saída
        var pathFile = Path.Combine(directoryName!, "Logo", "foto-relatorio-3.png");

        row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph($"YANN'S BARBERSHOP");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 25 };
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }

    private static void CreateTotalIncomesSection(Section page, DateOnly week, double totalIncomesValue)
    {
        var totalIncomesFormated = totalIncomesValue.ToString("C",
                  CultureInfo.CreateSpecificCulture("pt-BR"));

        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceAfter = 40;
        paragraph.Format.SpaceBefore = 40;

        paragraph.AddFormattedText($"{ResourceReportGenerationMessages.WEEKLY_REVENUE}", new Font { Name = FontHelper.ROBOTO_MEDIUM, Size = 15 });
        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{totalIncomesFormated}", new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 50 });
    }

    private static Table CreateIncomeTable(Section page)
    {
        var table = page.AddTable();
        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }
    private static void StyleForIncomeInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.SUPERLIGHT_GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private static void AddIncomeTitle(Cell cell, string titleIncome)
    {
        cell.AddParagraph(titleIncome);
        cell.Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.DARK_GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }
    private static void AddIncomeTitle(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.LIGHT_GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.RightIndent = 7;
    }

    private static void AddAmountForIncome(Cell cell, double amount)
    {
        var amountFormated = amount.ToString("C",
          CultureInfo.CreateSpecificCulture("pt-BR"));
        cell.AddParagraph($"{amountFormated}");
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private static byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
