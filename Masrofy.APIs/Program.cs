
using Masrofy.APIs.Extensions;
using Masrofy.APIs.Middlewares;
using Masrofy.Application;
using Masrofy.Persistence;
using System.Threading.Tasks;

namespace Masrofy.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddIdentityServices(builder.Configuration);

			builder.Services
				.AddPersistenceServices(builder.Configuration)
				.AddApplicationServices(builder.Configuration);

			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			await app.InitializeContextAsynce();

			app.UseMiddleware<ErrorHandlerMiddleware>();

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
