using Masrofy.APIs.Bases;
using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Features.Identity.Authentication.Commands.Models;
using Masrofy.Domain.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Masrofy.APIs.Controllers.Identity.Authentication
{
	public class AuthenticationController : BaseApiController
	{
		[HttpPost(Router.AuthenticationRouting.Login)]
		public async Task<ActionResult<ReturnUserDto>> Login([FromBody] LoginCommand loginCommand)
		{
			var result = await mediator.Send(loginCommand);
			return NewResult(result);
		}

	}
}
