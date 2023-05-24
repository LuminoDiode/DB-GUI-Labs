using api.Const;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[AllowAnonymous]
[Consumes("application/json")]
[Produces("application/json")]
[Route("/api/[controller]")]
public sealed class Lab1Controller : ControllerBase
{
	private readonly PgExecutor _executor;
	public Lab1Controller(PgExecutor executor)
	{
		_executor = executor;
	}


	[HttpPost]
	[Route("createDb")]
	public async Task<IActionResult> CreateDatabase()
	{
		try {
			await _executor.ExecuteAnyMaster(PremadeAirportRequests.Lab1.CreateDb);
			return Ok();
		} catch(Exception ex) {
			return Problem(ex.Message, statusCode: 500);
		}
	}

	[HttpPost]
	[Route("dropDb")]
	public async Task<IActionResult> DropDatabase()
	{
		try {
			await _executor.ExecuteAnyMaster(PremadeAirportRequests.Lab1.DropDb);
			return Ok();
		} catch(Exception ex) {
			return Problem(ex.Message);
		}
	}


	[HttpPost]
	[Route("createTables")]
	public async Task<IActionResult> CreateTables()
	{
		try {
			await _executor.ExecuteAny(PremadeAirportRequests.Lab1.CreateTables);
			return Ok();
		} catch(Exception ex) {
			return Problem(ex.Message);
		}
	}

	[HttpPost]
	[Route("generateData")]
	public async Task<IActionResult> CreateData()
	{
#if DEBUG
		try {
			await _executor.ExecuteAny(PremadeAirportRequests.Lab1.CopyStaticData);
			return Ok();
		} catch(Exception ex) {
			return Problem(ex.Message);
		}
#else
		throw new NotImplementedException();
#endif
	}
}