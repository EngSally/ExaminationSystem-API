using ExaminationSystem.Core.Dto;
using ExaminationSystem.Core.Model;
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
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManger;
		private readonly IConfiguration _configuration;

		public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger,IConfiguration configuration)	
		{
			_userManager=userManager;
			_roleManger=roleManger;
			_configuration=configuration;
		}
		[HttpPost("register")]
		public IActionResult Register(RegisterDTO userModel)
		{
			if(!ModelState.IsValid) { return BadRequest(ModelState); }
			return Ok();
		}



		[HttpPost("AllowEmail")]
		public async Task<IActionResult> AllowEmail(string Id, string email)
		{

			var user=await   _userManager.FindByEmailAsync(email);
			var isAllow= (user is null || user.Id == Id);
			return Ok(isAllow);
		}
		[HttpPost("AllowUserName")]
		public async Task<IActionResult> AllowUserName(string Id, string UserName)
		{
			var user =await _userManager.FindByNameAsync(UserName);
			var isAllow= (user is null || user.Id == Id);
			return Ok(isAllow);
		}
	}
}
