using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.Models.Identity.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Abstraction.Services.Authentication
{
	public interface IAuthService
	{
		Task<ReturnUserDto> Login(string email, string password);
		Task<ReturnUserDto> GetRefreshToken(RefreshDto refreshDto, CancellationToken cancellationToken);
		Task<bool> RevokeRefreshToken(RefreshDto refreshDto, CancellationToken cancellationToken);
	}
}
