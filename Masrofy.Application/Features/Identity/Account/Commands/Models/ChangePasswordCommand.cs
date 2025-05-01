using Masrofy.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Features.Identity.Account.Commands.Models
{
	public class ChangePasswordCommand : IRequest<Response<string>>
	{
		public required string UserId { get; set; }
		public required string OldPassword { get; set; }
		public required string NewPassword { get; set; }
		public ChangePasswordCommand(string userId, string oldPassword, string newPassword)
		{
			UserId = userId;
			OldPassword = oldPassword;
			NewPassword = newPassword;
		}
	}
}
