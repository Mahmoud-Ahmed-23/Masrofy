using Masrofy.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Persistence._Data.Configurations.Identity
{
	internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(x => x.FullName)
				.IsRequired()
				.HasMaxLength(100);


			builder.Property(x => x.ImageUrl)
				.IsRequired(false)
				.HasMaxLength(200);
		}
	}
}
