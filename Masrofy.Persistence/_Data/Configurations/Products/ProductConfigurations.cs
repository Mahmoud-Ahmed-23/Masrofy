using Masrofy.Persistence._Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Masrofy.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
namespace Masrofy.Persistence._Data.Configurations.ProductsConf
{
	internal class ProductConfigurations : BaseAuditableEntityConfigurations<Product, int>
	{
		public override void Configure(EntityTypeBuilder<Product> builder)
		{
			base.Configure(builder);

			builder.ToTable("Products");

			builder.HasKey(b => b.Id);

			builder.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(p => p.Description)
				.IsRequired();

			builder.Property(p => p.Price)
				.HasColumnType("decimal(9,2)");

			builder.HasOne(p => p.Brand)
				.WithMany()
				.HasForeignKey(p => p.BrandId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(p => p.Category)
				.WithMany()
				.HasForeignKey(p => p.CategoryId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.Property(p => p.NormlizedName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(p => p.QRCodeValue)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(p => p.PictureUrl)
				.HasMaxLength(200);

			builder.Property(p => p.Allowed)
				.HasDefaultValue(true);
		}
	}
}
