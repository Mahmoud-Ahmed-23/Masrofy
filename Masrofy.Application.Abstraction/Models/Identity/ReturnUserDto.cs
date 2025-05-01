using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Abstraction.Models.Identity
{
	public class ReturnUserDto
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }

		public string Status { get; set; }

		public string? PhoneNumber { get; set; }
		public string? Role { get; set; }

		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiryTime { get; set; }


	}
}
