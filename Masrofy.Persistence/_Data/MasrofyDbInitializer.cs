using Masrofy.Domain.Contracts.Persistence;
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
	}
}
