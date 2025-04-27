using Masrofy.Domain.Contracts.Persistence;

namespace Masrofy.APIs.Extensions
{
	public static class InitializeExtension
	{
		public static async Task InitializeContextAsynce(this WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var dbInitializer = services.GetRequiredService<IDbInitializer>();
					await dbInitializer.InitializeAsynce();
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while seeding the database.");
				}
			}
		}
	}
}
