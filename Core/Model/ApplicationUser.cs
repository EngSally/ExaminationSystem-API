using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.Core.Model
{
	[Index(nameof(Email), IsUnique = true)]
	[Index(nameof(UserName), IsUnique = true)]
	public class ApplicationUser : IdentityUser
	{
		[MaxLength(20)]
		public string FristName { get; set; } = null!;

		[MaxLength(20)]
		public string LastName { get; set; } = null!;
		public bool Deleted { get; set; }

		public string? CreatedById { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.Now;

		public string? LastUpdateById { get; set; }
		public DateTime? LastUpdateOn { get; set; }
	}
}
