using Masrofy.Domain.Contracts.Persistence;

namespace Masrofy.APIs.Extensions
{
	public static class InitializeExtension
	{
		public static async Task<WebApplication> InitializeContextAsynce(this WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var dbInitializer = services.GetRequiredService<IDbInitializer>();
					await dbInitializer.InitializeAsynce();
					await dbInitializer.SeedAsync();
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred duringx applaying migrations.");
				}
			}
			return app;
		}
	}
}
