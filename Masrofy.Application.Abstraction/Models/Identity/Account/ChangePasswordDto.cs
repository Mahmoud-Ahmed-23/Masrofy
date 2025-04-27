using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Abstraction.Models.Identity.Account
{
	public class ChangePasswordDto
	{
		public string Id { get; set; }
		public required string OldPassword { get; set; }
		public required string NewPassword { get; set; }
	}
}
