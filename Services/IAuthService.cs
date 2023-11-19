using ExaminationSystem.Core.Dto;

namespace ExaminationSystem.Services
{
	public interface IAuthService
	{
		 Task <AuthModelDto> Registration(RegisterDTO model);
		
	}
}
