using ExaminationSystem.Core.Dto;
using ExaminationSystem.Core.Model;
using ExaminationSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace ExaminationSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		
		private readonly IAuthService _authService;
		

		public AccountController  (IAuthService authService)	
		{
			_authService = authService;
			
			
		}
		[AllowAnonymous]
		[HttpPost("register")]
		public  async Task<IActionResult> Register(RegisterDTO userModel)
		{
			if(!ModelState.IsValid) return BadRequest(ModelState);
			var result = await _authService.RegistrationAsync(userModel);
			if (!result.IsAuthenticated)
				return BadRequest(result.Message);

			return Ok( new { 
				result.UserName,
				result.Token,
				result.ExpiresOn,
				result.Email
			});
		}
		[AllowAnonymous]
		[HttpPost("Login")]
		public  async Task <IActionResult> Login(LoginDTO model)
		{		
			if(!ModelState.IsValid) return BadRequest(ModelState);
			var result = await _authService.LoginAsync(model);
			if (!result.IsAuthenticated)
				return BadRequest(result.Message);

			return Ok(new
			{
				result.UserName,
				result.Token,
				result.ExpiresOn,
				result.Email
			});
		}
		[Authorize]
		[HttpGet("ChackToken")]
		public ActionResult  ChackToken()
		{
			return Ok("Uthorize");
		}





	}
}
