using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.Services.Authentication;
using Masrofy.Application.Abstraction.ServicesStatus;
using Masrofy.Application.Bases;
using Masrofy.Application.Features.Identity.Authentication.Commands.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Features.Identity.Authentication.Commands.Handlers
{
	public class AuthenticationCommandHandler
		: ResponseHandler,
		IRequestHandler<LoginCommand, Response<ReturnUserDto>>
	{
		private readonly IAuthService _authService;

		public AuthenticationCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<Response<ReturnUserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var user = await _authService.Login(request.Email, request.Password);

			if (user.Status == Status.NotFound)
				return NotFound<ReturnUserDto>("Login Failed");

			else if (user.Status == Status.LockedOut)
				return BadRequest<ReturnUserDto>("User is Locked Out");

			else if (user.Status == Status.EmailNotConfirmed)
				return BadRequest<ReturnUserDto>("Email is Not Confirmed");

			else if (user.Status == Status.Success)
				return Success(user, "Login Successfully :)");

			else
				return BadRequest<ReturnUserDto>("Bad Request");

		}
	}
}
