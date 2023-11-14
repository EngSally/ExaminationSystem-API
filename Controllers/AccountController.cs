using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		[HttpPost("register")]
		public IActionResult Register()
		{
			return Ok();
		}
	}
}
