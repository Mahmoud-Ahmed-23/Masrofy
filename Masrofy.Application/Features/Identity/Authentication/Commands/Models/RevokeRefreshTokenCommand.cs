using Masrofy.Application.Bases;
using MediatR;

namespace Masrofy.Application.Features.Identity.Authentication.Commands.Models
{
	public class RevokeRefreshTokenCommand : IRequest<Response<bool>>
	{
		public string Token { get; set; }
		public string RefreshToken { get; set; }

	}
}
