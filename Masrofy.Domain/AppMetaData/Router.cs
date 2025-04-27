using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Domain.AppMetaData
{
	public class Router
	{
		public const string root = "api";
		public const string version = "v1";
		public const string Rule = root + "/" + version + "/";
		public static class AccountRouting
		{
			public const string prefix = Rule + "account";

			public const string list = prefix + "/list";
			public const string id = prefix + "/{id}";
			public const string Edit = prefix + "/Edit";
			public const string Delete = prefix + "/{id}";

			public const string Register = prefix + "/Register";
			public const string SendCode = prefix + "/SendCode";
			public const string VerfiyCode = prefix + "/VerfiyCode";
			public const string ResetPassword = prefix + "/ResetPassword";
			public const string EmailConfirmation = prefix + "/EmailConfirmation";
			public const string ChangePassword = prefix + "/ChangePassword";
		}

		public static class AuthenticationRouting
		{
			public const string prefix = Rule + "Authentication";

			public const string Login = prefix + "/Login";

			public const string RefreshToken = prefix + "/RefreshToken";
			public const string RevokeRefreshToken = prefix + "/RevokeRefreshToken";
		}
	}
}
