using ExaminationSystem.Core.Const;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExaminationSystem.Core.Dto
{
	public class RegisterDTO
	{
		public string? Id { get; set; } 
		public string UserName { get; set; }= null!;
		public string FristName { get; set; }= null!;
		public string LastName { get; set; } = null!;
		
		[EmailAddress, MaxLength(100, ErrorMessage = Errors.MaxLength)]
		public string Email { get; set; } = null!;
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string PasswordConfirmed { get; set; } = null!;
		[AllowedValues(Roles.Student,Roles.Teacher,Roles.Admin,
	   ErrorMessage = $"Only {Roles.Student},{Roles.Teacher},{Roles.Admin} Roles are allowed")]
		public string  Role { get; set; } 

	}
}
