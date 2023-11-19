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
		public  async Task<IActionResult> Register(RegisterDTO userModel)
		{
			if(!ModelState.IsValid) { return BadRequest(ModelState); }
			if (await _userManager.FindByEmailAsync(userModel.Email)  is not null)
			{
				return BadRequest("Email Is Found Befor");
			}
			if (await _userManager.FindByEmailAsync(userModel.UserName) is not null)
			{
				return BadRequest("UserName Is Found Befor");
			}
			var user  = new ApplicationUser()
			{
				UserName = userModel.UserName,
				Email = userModel.Email,
				FristName= userModel.FristName,
				LastName= userModel.LastName
			};
		var resut=	 await _userManager.CreateAsync(user);
			if (!resut.Succeeded) { return BadRequest(); }


			return Ok();
		}

		[HttpPost("Login")]
		public  IActionResult Login(LoginDTO model)
		{
			return Unauthorized("In valid userName");
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
