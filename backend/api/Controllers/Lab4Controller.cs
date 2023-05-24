using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[AllowAnonymous]
[Consumes("application/json")]
[Produces("application/json")]
[Route("/api/[controller]")]
public class Lab4Controller : ControllerBase
{
	private readonly PgExecutor _executor;
	public Lab4Controller(PgExecutor executor)
	{
		_executor = executor;
	}

	[HttpPost]
	[Route("timeTable")]
	public async Task<IActionResult> CreateTimeTable([FromQuery] string from, [FromQuery] string to)
	{
		try {
			return Ok(await _executor.L4_CreateTimeTable(from, to));
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}

	[HttpPost]
	[Route("changeCapacity")]
	public async Task<IActionResult> ChangeCapacity([FromQuery] string brand, [FromQuery] int delta)
	{
		try {
			return Ok(await _executor.L4_ChangeCapacity(brand, delta));
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}


	[HttpPost]
	[Route("FlightsOnRoute")]
	public async Task<IActionResult> FlightsOnRoute()
	{
		try {
			return Ok(await _executor.L4_FlightsOnRoute());
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}


	[HttpPost]
	[Route("ChangeSpeed")]
	public async Task<IActionResult> ChangeSpeed([FromQuery] string brand, [FromQuery] int delta)
	{
		try {
			return Ok(await _executor.L4_ChangeSpeed(brand, delta));
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}
}
