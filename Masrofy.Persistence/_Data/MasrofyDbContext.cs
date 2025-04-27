using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Masrofy.Persistence._Data
{
	public class MasrofyDbContext : IdentityDbContext
	{
		public MasrofyDbContext(DbContextOptions options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(AssymblyInformation).Assembly);
		}
	}
}
