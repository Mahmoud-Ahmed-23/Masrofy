using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Domain.Contracts.Persistence
{
	public interface IDbInitializer
	{
		Task InitializeAsynce();
	}
}