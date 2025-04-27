using Masrofy.Application.Abstraction.Models.Identity.Account;
using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.ServicesStatus;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Masrofy.Application.Abstraction.Services.Authentication;
using Masrofy.Domain.Entities.Identity;
using Masrofy.Shared.Settings;
using Masrofy.Application.Abstraction.Services.Account;

namespace Masrofy.Application.Services.Account
{
	internal class AccountService(
		UserManager<ApplicationUser> _userManager,
		IMapper _mapper
		) : IAccountService
	{


		public async Task<ReturnUserDto> Register(CreateUserDto userDto)
		{
			var user = await _userManager.FindByEmailAsync(userDto.Email);

			ReturnUserDto returnUser;

			if (user is not null)
				return returnUser = new ReturnUserDto()
				{
					Status = Status.UserAlreadyExists
				};

			var createdUser = _mapper.Map<ApplicationUser>(userDto);

			var created = await _userManager.CreateAsync(createdUser, userDto.Password);

			if (!created.Succeeded)
			{
				return returnUser = new ReturnUserDto()
				{
					Status = Status.UserCreationFailed
				};
			}

			returnUser = _mapper.Map<ReturnUserDto>(createdUser);

			returnUser.Status = Status.Success;


			var role = await _userManager.AddToRoleAsync(createdUser!, userDto.Role);

			if (!role.Succeeded)
			{
				returnUser.Status = Status.UserCreationFailed;
			}

			return returnUser;

		}

		public Task<string> ChangePassword(ChangePasswordDto changePasswordDto)
		{
			throw new NotImplementedException();
		}
		public Task<ReturnUserDto> UpdateUser(UpdateUserDto userDto)
		{
			throw new NotImplementedException();
		}
	}
}
