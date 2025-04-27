using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Abstraction.ServicesStatus
{
	public static class Status
	{
		public static string NotFound = "NotFound";
		public static string BadRequest = "BadRequest";
		public static string Unauthorized = "Unauthorized";
		public static string Success = "Success";


		#region Identity

		public static string UserAlreadyExists = "UserAlreadyExists";
		public static string UserCreationFailed = "UserCreationFailed";
		public static string LockedOut = "LockedOut";
		public static string EmailNotConfirmed = "EmailNotConfirmed";

		#endregion

	}
}
