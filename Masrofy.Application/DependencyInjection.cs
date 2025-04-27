using FluentValidation;
using Masrofy.Application.Abstraction.Services.Account;
using Masrofy.Application.Abstraction.Services.Authentication;
using Masrofy.Application.Services.Account;
using Masrofy.Application.Services.Authentication;
using Masrofy.Shared.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			var JwtSettings = new JwtSettings();

			configuration.GetSection(nameof(JwtSettings)).Bind(JwtSettings);

			services.AddSingleton(JwtSettings);

			services.AddScoped(typeof(IAccountService), typeof(AccountService));

			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			return services;
		}
	}
}
