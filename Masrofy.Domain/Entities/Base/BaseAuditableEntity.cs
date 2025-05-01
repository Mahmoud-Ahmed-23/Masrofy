using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Domain.Entities.Base
{
	public class BaseAuditableEntity<TKey> : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastMoifiedOn { get; set; }
		public string LastModifiedBy { get; set; }
	}
}
