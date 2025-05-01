using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Abstraction.Models.Identity.Account
{
	public class UpdateUserDto
	{
		public required string Id { get; set; }
		public required string FullName { get; set; }
		public required string Email { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
