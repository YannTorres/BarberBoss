using BarberBoss.Application.UseCases.Users.Login;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromBody] RequestLoginJson request,
        [FromServices] ILoginUseCase useCase
        )
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
