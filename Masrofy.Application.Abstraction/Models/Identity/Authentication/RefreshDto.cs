using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Abstraction.Models.Identity.Authentication
{
	public class RefreshDto
	{
		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
	}
}
