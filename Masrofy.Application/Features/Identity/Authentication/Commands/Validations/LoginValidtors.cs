using FluentValidation;
using Masrofy.Application.Abstraction.Patterns;
using Masrofy.Application.Features.Identity.Authentication.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Features.Identity.Authentication.Commands.Validations
{
	public class LoginValidtors : AbstractValidator<LoginCommand>
	{
		public LoginValidtors()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("Email is required")
				.Matches(ValidatorPatterns.Email)
				.WithMessage("Email is not valid");

		}
	}
}
