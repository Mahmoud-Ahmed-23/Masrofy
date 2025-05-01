using Masrofy.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Domain.Entities.Products
{
	public class Product : BaseAuditableEntity<int>
	{
		public required string Name { get; set; }

		public required string NormlizedName { get; set; }
		public required string Description { get; set; }
		public string? PictureUrl { get; set; }
		public decimal Price { get; set; }

		public int? BrandId { get; set; }
		public virtual Brand? Brand { get; set; }

		public int? CategoryId { get; set; }
		public virtual Category? Category { get; set; }

		public required string QRCodeValue { get; set; }

		public bool Allowed { get; set; }

	}
}
