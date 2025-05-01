using Masrofy.Domain.Contracts.Persistence;
using Masrofy.Domain.Entities.Base;
using Masrofy.Persistence._Data;
using Masrofy.Persistence.Repositories.GenericRepository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Persistence.UnitOfWork
{
	internal class UnitOfWork : IUnitOfWork
	{
		private readonly MasrofyDbContext _dbContext;
		private readonly ConcurrentDictionary<string, object> _repos;

		public UnitOfWork(MasrofyDbContext dbContext)
		{
			_dbContext = dbContext;
			_repos = new();
		}
		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseAuditableEntity<TKey>
			where TKey : IEquatable<TKey>
		{
			return (IGenericRepository<TEntity, TKey>)_repos.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));
		}


		public async Task<int> CompleteAsync()
		=> await _dbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync()
		=> await _dbContext.DisposeAsync();
	}
}
