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
		IRequestHandler<RegisterCommand, Response<ReturnUserDto>>,
		IRequestHandler<EditUserCommand, Response<ReturnUserDto>>,
		IRequestHandler<ChangePasswordCommand, Response<string>>
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

		public async Task<Response<ReturnUserDto>> Handle(EditUserCommand request, CancellationToken cancellationToken)
		{
			var userDto = request.UpdateUserDto;

			var result = await _accountService.UpdateUser(userDto);

			if (result.Status == Status.NotFound)
				return BadRequest<ReturnUserDto>("User Not Found");

			if (result.Status == Status.BadRequest)
				return BadRequest<ReturnUserDto>("User Update Failed");

			return Success(result, "User Updated Successfully :)");
		}

		public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
		{
			var changePasswordDto = new ChangePasswordDto
			{
				Id = request.UserId,
				OldPassword = request.OldPassword,
				NewPassword = request.NewPassword
			};

			var result = await _accountService.ChangePassword(changePasswordDto);

			if (result is null)
				return BadRequest<string>("User Not Found");

			if (result == "BadRequest")
				return BadRequest<string>("User Update Failed");

			return Success(result, "User Updated Successfully :)");
		}
	}
}
