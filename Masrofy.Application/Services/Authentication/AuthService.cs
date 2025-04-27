using AutoMapper;
using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.Services.Authentication;
using Masrofy.Application.Abstraction.ServicesStatus;
using Masrofy.Domain.Entities.Identity;
using Masrofy.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Masrofy.Application.Services.Authentication
{
	internal class AuthService
		(
		UserManager<ApplicationUser> _userManager,
		SignInManager<ApplicationUser> _signInManager,
		IMapper _mapper,
		JwtSettings _jwtSettings
		) : IAuthService
	{

		public async Task<ReturnUserDto> Login(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user is null)
			{
				return new ReturnUserDto()
				{
					Status = Status.NotFound
				};
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

			if (!user.EmailConfirmed)
			{
				return new ReturnUserDto()
				{
					Status = Status.EmailNotConfirmed
				};
			}
			if (result.IsLockedOut)
			{
				return new ReturnUserDto()
				{
					Status = Status.LockedOut
				};
			}

			if (!result.Succeeded)
			{
				return new ReturnUserDto()
				{
					Status = Status.BadRequest
				};
			}

			var returnUser = _mapper.Map<ReturnUserDto>(user);

			returnUser.Status = Status.Success;

			returnUser.Token = await GenerateToken(user);

			return returnUser;

		}

		private async Task<string> GenerateToken(ApplicationUser user)
		{
			var roles = await _userManager.GetRolesAsync(user);
			var userClaims = await _userManager.GetClaimsAsync(user);

			var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Email, user.Email!),
				new Claim(ClaimTypes.Name, user.FullName)
			};


			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInDays),
				claims: claims,
				signingCredentials: creds
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}
