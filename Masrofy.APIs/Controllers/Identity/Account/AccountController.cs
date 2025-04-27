using Masrofy.APIs.Bases;
using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.Models.Identity.Account;
using Masrofy.Application.Features.Identity.Account.Commands.Models;
using Masrofy.Domain.AppMetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Masrofy.APIs.Controllers.Identity.Account
{

	public class AccountController : BaseApiController
	{
		[HttpPost(Router.AccountRouting.Register)]
		public async Task<ActionResult<ReturnUserDto>> Register([FromBody] RegisterCommand command)
		{
			var result = await mediator.Send(command);
			return NewResult(result);
		}
	}
}
