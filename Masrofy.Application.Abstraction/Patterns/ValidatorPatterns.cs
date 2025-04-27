using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Abstraction.Patterns
{
	public class ValidatorPatterns
	{
		public const string PhoneNumber = @"^(\+2)?(01[0-2,5]\d{8}|02\d{8}|03\d{7})$";
		public const string Email = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|googlemail\.com|google\.com|[a-zA-Z0-9-]+\.edu\.eg)$";
		public const string Password = @"^(?=.*\d)[\w!@#$%^&*()\-+={}[\]:;""'<>,.?/\\|`~]{8,}$";
	}
}
