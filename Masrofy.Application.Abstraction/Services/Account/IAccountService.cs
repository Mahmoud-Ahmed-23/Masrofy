using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.Models.Identity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Abstraction.Services.Account
{
	public interface IAccountService
	{
		Task<ReturnUserDto> Register(CreateUserDto userDto);

		Task<ReturnUserDto> UpdateUser(UpdateUserDto userDto);
		Task<string> ChangePassword(ChangePasswordDto changePasswordDto);
	}
}
