using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Features.Identity.Authentication.Commands.Models
{
	public class LoginCommand : IRequest<Response<ReturnUserDto>>
	{
		public LoginCommand(string email, string password)
		{
			Email = email;
			Password = password;
		}

		public string Email { get; set; }
		public string Password { get; set; }
	}
}
