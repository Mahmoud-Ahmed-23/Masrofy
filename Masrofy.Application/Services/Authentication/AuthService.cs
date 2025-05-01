using AutoMapper;
using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.Models.Identity.Authentication;
using Masrofy.Application.Abstraction.Services.Authentication;
using Masrofy.Application.Abstraction.ServicesStatus;
using Masrofy.Domain.Entities.Identity;
using Masrofy.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

			returnUser.Token = await GenerateTokenAsync(user);

			await CheckRefreshToken(_userManager, user, returnUser);

			return returnUser;

		}

		private async Task<string> GenerateTokenAsync(ApplicationUser user)
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

		private RefreshToken GenerateRefreshToken()
		{
			var randomNumber = new byte[32];

			var gen = RandomNumberGenerator.Create();

			gen.GetBytes(randomNumber);

			return new RefreshToken()
			{
				Token = Convert.ToBase64String(randomNumber),
				CreatedOn = DateTime.UtcNow,
				ExpireOn = DateTime.UtcNow.AddDays(_jwtSettings.JWTRefreshTokenExpire),
			};

		}

		private async Task CheckRefreshToken(UserManager<ApplicationUser> userManager, ApplicationUser user, ReturnUserDto response)
		{
			if (user.RefreshTokens.Any(t => t.IsActive))
			{
				var activeToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);

				if (activeToken is not null)
				{
					response.RefreshToken = activeToken.Token;
					response.RefreshTokenExpiryTime = activeToken.ExpireOn;
				}
			}
			else
			{
				var newRefreshToken = GenerateRefreshToken();
				response.RefreshToken = newRefreshToken.Token;
				response.RefreshTokenExpiryTime = newRefreshToken.ExpireOn;
				user.RefreshTokens.Add(newRefreshToken);
				await userManager.UpdateAsync(user);
			}
		}

		private string? ValidateToken(string token)
		{
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var tokenHandler = new JwtSecurityTokenHandler();

			try
			{
				var validationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = authKey,
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = false,
					ClockSkew = TimeSpan.Zero
				};

				var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

				var jwtToken = validatedToken as JwtSecurityToken;

				if (jwtToken != null)
				{
					var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

					if (userId != null)
						return userId;
					return null;
				}
				return null;
			}
			catch
			{
				return null;
			}

		}

		public async Task<ReturnUserDto> GetRefreshToken(RefreshDto refreshDto, CancellationToken cancellationToken)
		{
			var userId = ValidateToken(refreshDto.Token);

			if (userId is null)
				return new ReturnUserDto
				{
					Status = Status.NotFound
				};

			var user = await _userManager.FindByIdAsync(userId);

			if (user is null)
				return new ReturnUserDto
				{
					Status = Status.NotFound
				};

			var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == refreshDto.RefreshToken && x.IsActive);

			if (refreshToken is null)
				return new ReturnUserDto
				{
					Status = Status.TokenNotFound
				};

			refreshToken.RevokedOn = DateTime.UtcNow;

			var newToken = await GenerateTokenAsync(user);

			var newRefreshToken = GenerateRefreshToken();

			user.RefreshTokens.Add(newRefreshToken);

			await _userManager.UpdateAsync(user);

			var userDto = new ReturnUserDto
			{
				FullName = user.FullName,
				Status = Status.Success,
				Email = user.Email!,
				Id = user.Id,
				PhoneNumber = user.PhoneNumber!,
				Token = newToken,
				RefreshToken = newRefreshToken.Token,
				RefreshTokenExpiryTime = newRefreshToken.ExpireOn
			};

			return userDto;
		}

		public async Task<bool> RevokeRefreshToken(RefreshDto refreshDto, CancellationToken cancellationToken)
		{
			var userId = ValidateToken(refreshDto.Token!);

			if (userId is null)
				return false;

			var user = await _userManager.FindByIdAsync(userId);

			if (user is null)
				return false;

			var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == refreshDto.RefreshToken && x.IsActive);

			if (refreshToken is null)
				return false;

			refreshToken.RevokedOn = DateTime.UtcNow;

			await _userManager.UpdateAsync(user);

			return true;
		}
	}
}
