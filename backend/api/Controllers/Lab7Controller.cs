using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[AllowAnonymous]
[Consumes("application/json")]
[Produces("application/json")]
[Route("/api/[controller]")]
public class Lab7Controller : ControllerBase
{
	private readonly PgExecutor _executor;
	public Lab7Controller(PgExecutor executor)
	{
		_executor = executor;
	}

	[HttpPost]
	[Route("changeTicket")]
	public async Task<IActionResult> SelectByBrandAndCapacity([FromQuery] int from, [FromQuery] int to)
	{
		return await _executor.L7_ChangeTicket(from, to) ?
			Ok() : Problem();
	}

}
