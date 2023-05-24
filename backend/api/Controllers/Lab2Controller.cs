using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;


[ApiController]
[AllowAnonymous]
[Consumes("application/json")]
[Produces("application/json")]
[Route("/api/[controller]")]
public class Lab2Controller : ControllerBase
{
	private readonly PgExecutor _executor;
	public Lab2Controller(PgExecutor executor)
	{
		_executor = executor;
	}

	[HttpPost]
	[Route("AvgTime")]
	public async Task<IActionResult> AvgFlyTime([FromQuery] string from, [FromQuery] string to, [FromQuery] string brand)
	{
		try {
			return Ok(await _executor.L2_AvgFlyTime(brand, from, to));
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}

	[HttpPost]
	[Route("MostlySameRoute")]
	public async Task<IActionResult> MostlySameRoute()
	{
		try {
			return Ok(await _executor.L2_MostlySameRoute());
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}

	[HttpPost]
	[Route("mostlyEmpty70")]
	public async Task<IActionResult> MostlyEmpty70()
	{
		try {
			return Ok(await _executor.L2_MostlyEmpty70());
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}

	[HttpPost]
	[Route("FreeSeats")]
	public async Task<IActionResult> FreeSeats([FromQuery] int route, [FromQuery] string date)
	{
		try {
			return Ok(await _executor.L2_FreeSeats(route, DateTime.Parse(date)));
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}
}
