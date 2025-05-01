using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.Models.Identity.Authentication;
using Masrofy.Application.Abstraction.Services.Authentication;
using Masrofy.Application.Abstraction.ServicesStatus;
using Masrofy.Application.Bases;
using Masrofy.Application.Features.Identity.Authentication.Commands.Models;
using MediatR;

namespace Masrofy.Application.Features.Identity.Authentication.Commands.Handlers
{
	public class AuthenticationCommandHandler
		: ResponseHandler,
		IRequestHandler<LoginCommand, Response<ReturnUserDto>>,
		IRequestHandler<RefreshTokenCommand, Response<ReturnUserDto>>,
		IRequestHandler<RevokeRefreshTokenCommand, Response<bool>>
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

		public async Task<Response<ReturnUserDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var user = await _authService.GetRefreshToken(new RefreshDto { Token = request.Token, RefreshToken = request.RefreshToken }, cancellationToken);

			if (user.Status == Status.NotFound)
				return NotFound<ReturnUserDto>("User is Not Found");

			else if (user.Status == Status.TokenNotFound)
				return BadRequest<ReturnUserDto>("Token Not Found");

			else if (user.Status == Status.Success)
				return Success(user, "Refresh Token Successfully :)");
			else
				return BadRequest<ReturnUserDto>("Bad Request");
		}

		public async Task<Response<bool>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var result = await _authService.RevokeRefreshToken(new RefreshDto { Token = request.Token, RefreshToken = request.RefreshToken }, cancellationToken);
			
			if (result)
				return Success(true, "Refresh Token Revoked Successfully :)");
			else
				return BadRequest<bool>("Bad Request");
		}
	}
}
