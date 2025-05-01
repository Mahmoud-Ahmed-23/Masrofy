using FluentValidation;
using Masrofy.Application.Abstraction.Patterns;
using Masrofy.Application.Features.Identity.Account.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Features.Identity.Account.Commands.Validators
{
	public class RegisterValidator : AbstractValidator<RegisterCommand>
	{
		public RegisterValidator()
		{

			ApplyValidator();
		}
		public void ApplyValidator()
		{
			RuleFor(x => x.FullName)
				.NotEmpty()
				.WithMessage("Full Name is required");

			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("Email is required")
				.Matches(ValidatorPatterns.Email)
				.WithMessage("Email is not valid");

			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Password is required")
				.Matches(ValidatorPatterns.Password)
				.WithMessage("Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character");

			RuleFor(x => x.PhoneNumber)
				.NotEmpty()
				.WithMessage("Phone Number is required")
				.Matches(ValidatorPatterns.PhoneNumber)
				.WithMessage("Phone Number is not valid");

			RuleFor(x => x.Role)
				.NotEmpty()
				.WithMessage("Role is required");

		}

	}
}
