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
	public class RefreshTokenCommand : IRequest<Response<ReturnUserDto>>
	{
		public string RefreshToken { get; set; }
		public string Token { get; set; }
		
	}
}
