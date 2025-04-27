using AutoMapper;
using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.Models.Identity.Account;
using Masrofy.Application.Abstraction.Services.Account;
using Masrofy.Application.Abstraction.ServicesStatus;
using Masrofy.Application.Bases;
using Masrofy.Application.Features.Identity.Account.Commands.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Features.Identity.Account.Commands.Handlers
{
	internal class AccountCommandHandler
		: ResponseHandler,
		IRequestHandler<RegisterCommand, Response<ReturnUserDto>>
	{
		private readonly IAccountService _accountService;
		private readonly IMapper _mapper;

		public AccountCommandHandler(IAccountService accountService, IMapper mapper)
		{
			_accountService = accountService;
			_mapper = mapper;
		}

		public async Task<Response<ReturnUserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
		{
			var userDto = _mapper.Map<CreateUserDto>(request);

			var result = await _accountService.Register(userDto);

			if (result.Status == Status.UserAlreadyExists)
				return BadRequest<ReturnUserDto>("User with this Email is already Exists");

			if (result.Status == Status.UserCreationFailed)
				return BadRequest<ReturnUserDto>("User Creation Failed");

			return Success(result, "Register Successfully :)");
		}
	}
}
