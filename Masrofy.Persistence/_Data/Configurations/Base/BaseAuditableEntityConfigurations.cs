using Masrofy.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Persistence._Data.Configurations.Base
{
	public class BaseAuditableEntityConfigurations<TEntity,TKey>:BaseEntityConfigurations<TEntity,TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public override void Configure(EntityTypeBuilder<TEntity> builder)
		{
			base.Configure(builder);

			builder.Property(b => b.CreatedOn)
				.IsRequired();

			builder.Property(b => b.CreatedBy)
				.IsRequired();

			builder.Property(b => b.LastMoifiedOn)
				.IsRequired();

			builder.Property(b => b.LastModifiedBy)
				.IsRequired();
		}
	}
}
