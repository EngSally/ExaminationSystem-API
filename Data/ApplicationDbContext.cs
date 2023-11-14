using ExaminationSystem.Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Data
{
	public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext() { }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
	}
}
