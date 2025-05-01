using Masrofy.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Domain.Contracts.Persistence
{
	public interface IGenericRepository<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		Task<IEnumerable<TEntity>> GetAllAsync(bool withTraking = true);

		Task<TEntity> GetByIdAsync(TKey id);

		Task AddAsync(TEntity entity);

		void Update(TEntity entity);

		void Delete(TKey id);
	}
}
