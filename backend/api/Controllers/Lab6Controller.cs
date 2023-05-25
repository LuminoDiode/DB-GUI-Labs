using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[AllowAnonymous]
[Consumes("application/json")]
[Produces("application/json")]
[Route("/api/[controller]")]
public class Lab6Controller : ControllerBase
{
	private readonly PgExecutor _executor;
	public Lab6Controller(PgExecutor executor)
	{
		_executor = executor;
	}

	[HttpPost]
	[Route("byBrandAndCapcity")]
	public async Task<IActionResult> SelectByBrandAndCapacity([FromQuery] string[]brand, [FromQuery] int  min, [FromQuery] int max)
	{
		try {
			return Ok(await _executor.L6_SelectByBrandAndCapacity(brand, min,max));
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}

	[HttpPost]
	[Route("ByLastBoardDigit")]
	public async Task<IActionResult> SelectByLastBoardDigit([FromQuery] char min, [FromQuery] char max)
	{
		try {
			return Ok(await _executor.L6_SelectByLastBoardDigit(min, max));
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}

	[HttpPost]
	[Route("ByDepartureAndFirstChar")]
	public async Task<IActionResult> ByDepartureAndFirstChar([FromQuery] char first)
	{
		try {
			return Ok(await _executor.L6_DepartureAndFirstChar(first));
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}

	[HttpPost]
	[Route("WhereSoldN")]
	public async Task<IActionResult> WhereSoldN([FromQuery] int quantity)
	{
		try {
			return Ok(await _executor.L6_WhereSoldN(quantity));
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}
}
