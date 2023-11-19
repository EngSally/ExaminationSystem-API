using ExaminationSystem.Core.Dto;
using ExaminationSystem.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExaminationSystem.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManger;
		private readonly IConfiguration _configuration;
		public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger, IConfiguration configuration)
		{
			_userManager = userManager;
			_roleManger = roleManger;
			_configuration = configuration;
		}
		public async Task<AuthModelDto> Registration(RegisterDTO model)
		{
			if (await _userManager.FindByEmailAsync(model.Email) is not null)
				return new AuthModelDto { Message = "Email Is Found Befor" };
			if (await _userManager.FindByEmailAsync(model.UserName) is not null)
				return new AuthModelDto { Message = "UserName Is Found Befor" };
			if (await _roleManger.FindByNameAsync(model.Role) is null)
				return new AuthModelDto { Message = $"No Role With Name {model.Role}" };
			var user  = new ApplicationUser()
			{
				UserName = model.UserName,
				Email    = model.Email,
				FristName= model.FristName,
				LastName=  model.LastName
			};
			var resut=   await _userManager.CreateAsync(user,model.Password);
			if (!resut.Succeeded)
			{
				var errors=string .Empty;
				Parallel.ForEach(resut.Errors, err =>
				{
					errors += $"{err.Description} ,";
				});
				return new AuthModelDto { Message = errors };
			}

			var ss=await _userManager.AddToRoleAsync(user, model.Role);
			var jwtSecurityToken = await CreateJwtToken(user);

			return new AuthModelDto
			{
				Email = user.Email,
				ExpiresOn = jwtSecurityToken.ValidTo,
				IsAuthenticated = true,
			    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				UserName = user.UserName
			};
		}

		private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email!),
				new Claim("uid", user.Id)
			};
			
			

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _configuration["JWT:Issuer"],
				audience: _configuration["JWT:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(int.Parse( _configuration["JWT:ClockSkew"]!)),
				signingCredentials: signingCredentials);

			return jwtSecurityToken;
		}
	}
}
