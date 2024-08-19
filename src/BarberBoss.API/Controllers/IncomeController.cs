using BarberBoss.Application.UseCases.Income.Delete;
using BarberBoss.Application.UseCases.Income.GetAll;
using BarberBoss.Application.UseCases.Income.GetById;
using BarberBoss.Application.UseCases.Income.Register;
using BarberBoss.Application.UseCases.Income.Update;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Response;
using BarberBoss.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class IncomeController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredIncomeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterIncome
        ([FromServices] IRegisterIncomeUseCase useCase,
         [FromBody] RequestIncomeJson request
        )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllIncomes([FromServices] IGetAllIncomesUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Incomes.Count == 0)
            return NoContent();

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetIncomeById(
        [FromRoute] int id,
        [FromServices] IGetIncomeByIdUseCase useCase
        )
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIncome([FromRoute] int id, [FromServices] IDeleteIncomeUseCase useCase)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateIncome(
        [FromRoute] int id,
        [FromServices] IUpdateIncomeUseCase useCase,
        [FromBody] RequestIncomeJson request
        )
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
