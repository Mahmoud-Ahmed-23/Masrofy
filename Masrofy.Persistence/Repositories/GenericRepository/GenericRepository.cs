using Masrofy.Domain.Contracts.Persistence;
using Masrofy.Domain.Entities.Base;
using Masrofy.Persistence._Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Persistence.Repositories.GenericRepository
{
	internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		private readonly MasrofyDbContext _dbContext;

		public GenericRepository(MasrofyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAsync(TEntity entity)
			=> await _dbContext.Set<TEntity>().AddAsync(entity);

		public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTraking)
			=> await (withTraking ? _dbContext.Set<TEntity>() : _dbContext.Set<TEntity>().AsNoTracking()).ToListAsync();

		public async Task<TEntity> GetByIdAsync(TKey id)
		=> await _dbContext.Set<TEntity>().FindAsync(id);

		public void Update(TEntity entity)
		=> _dbContext.Set<TEntity>().Update(entity);

		public void Delete(TKey id)
			=> _dbContext.Set<TEntity>().Remove(_dbContext.Set<TEntity>().Find(id)!);
	}
}
