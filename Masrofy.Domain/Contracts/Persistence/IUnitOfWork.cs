using Masrofy.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Domain.Contracts.Persistence
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseAuditableEntity<TKey>
			where TKey : IEquatable<TKey>;
		Task<int> CompleteAsync();
	}
}
