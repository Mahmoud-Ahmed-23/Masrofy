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
	public class EditUserCommand : IRequest<Response<ReturnUserDto>>
	{
		public UpdateUserDto UpdateUserDto { get; set; }

		public EditUserCommand(UpdateUserDto updateUserDto)
		{
			UpdateUserDto = updateUserDto;
		}
	}
}
