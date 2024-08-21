using BarberBoss.Application.UseCases.Income.Reports.Excel;
using BarberBoss.Application.UseCases.Income.Reports.PDF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BarberBoss.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateIncomesReportExcelUseCase useCase,
        [FromHeader] DateOnly dateOnly
    )
    {
        byte[] file = await useCase.Execute(dateOnly);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf(
        [FromServices] IGenerateIncomesReportPdfUseCase useCase,
        [FromHeader] DateOnly dateOnly
    )
    {
        byte[] file = await useCase.Execute(dateOnly);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();
    }
}
