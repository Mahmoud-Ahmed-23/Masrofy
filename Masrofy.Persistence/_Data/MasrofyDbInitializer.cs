using Masrofy.Domain.Contracts.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Masrofy.Persistence._Data
{
	internal class MasrofyDbInitializer(MasrofyDbContext _dbContext) : IDbInitializer
	{
		public async Task InitializeAsynce()
		{
			var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

			if (pendingMigrations.Any())
			{
				await _dbContext.Database.MigrateAsync();
			}

		}

		public async Task SeedAsync()
		{

			if (!await _dbContext.Roles.AnyAsync())
			{
				await _dbContext.Roles.AddRangeAsync(new List<IdentityRole>()
				{
					new IdentityRole
					{
						Name = "Admin",
						NormalizedName = "ADMIN"
					},
					new IdentityRole
					{
						Name = "Child",
						NormalizedName = "CHILD"
					},
					new IdentityRole
					{
						Name = "Parent",
						NormalizedName = "PARENT"
					}

				});
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
