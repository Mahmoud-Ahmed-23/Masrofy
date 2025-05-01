using Masrofy.Domain.Entities.Products;
using Masrofy.Persistence._Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Persistence._Data.Configurations.ProductsConf
{
	internal class BrandConfigurations : BaseAuditableEntityConfigurations<Brand, int>
	{
		public override void Configure(EntityTypeBuilder<Brand> builder)
		{
			base.Configure(builder);

			builder.ToTable("Brands");

			builder.HasKey(b => b.Id);

			builder.Property(b => b.Name)
				.IsRequired();
		}
	}
}
