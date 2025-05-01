using Masrofy.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Domain.Entities.Products
{
	public class Brand : BaseAuditableEntity<int>
	{
		public required string Name { get; set; }
	}
}
