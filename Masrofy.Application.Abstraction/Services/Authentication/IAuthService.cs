using Masrofy.Application.Abstraction.Models.Identity;
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
	}
}
