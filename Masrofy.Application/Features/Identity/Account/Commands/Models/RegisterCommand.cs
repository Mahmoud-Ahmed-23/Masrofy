using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.Models.Identity.Account;
using Masrofy.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Features.Identity.Account.Commands.Models
{
	public class RegisterCommand : IRequest<Response<ReturnUserDto>>
	{
		public required string FullName { get; set; }

		public required string Email { get; set; }
		public string Password { get; set; }

		public string? PhoneNumber { get; set; }
		public string Role { get; set; }

		public RegisterCommand(string fullName, string email, string password, string? phoneNumber, string role)
		{
			FullName = fullName;
			Email = email;
			Password = password;
			PhoneNumber = phoneNumber;
			Role = role;
		}

	}
}
