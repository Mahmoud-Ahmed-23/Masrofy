using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Domain.Entities.Base
{
	public class BaseEntity<TKey> where TKey : IEquatable<TKey>

	{
		public required TKey Id { get; set; }
	}
}
