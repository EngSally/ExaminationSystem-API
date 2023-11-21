using ExaminationSystem.Core.Dto;

namespace ExaminationSystem.Services
{
	public interface IAuthService
	{
		 Task <AuthModelDto> RegistrationAsync(RegisterDTO model);
		Task<AuthModelDto> LoginAsync(LoginDTO loginDTO);
		
	}
}
