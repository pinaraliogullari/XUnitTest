using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XUnitTest.Mvc.Data.Entities;

namespace XUnitTest.Mvc.Data.Config
{
	public class ProductConfig : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
			builder.Property(x => x.Color).IsRequired().HasMaxLength(200);
			builder.Property(x => x.Price).IsRequired();
			builder.Property(x => x.Stock).IsRequired();

			builder.HasData(
				new Product() { Id = Guid.NewGuid(), Name = "Product 1", Color = "Red", Stock = 100, Price = 2000 },
				new Product() { Id = Guid.NewGuid(), Name = "Product 2", Color = "Black", Stock = 150, Price = 3000 },
				new Product() { Id = Guid.NewGuid(), Name = "Product 3", Color = "Yellow", Stock = 200, Price = 4000 },
				new Product() { Id = Guid.NewGuid(), Name = "Product 4", Color = "Green", Stock = 250, Price = 5000 }
				);
		}
	}
}
