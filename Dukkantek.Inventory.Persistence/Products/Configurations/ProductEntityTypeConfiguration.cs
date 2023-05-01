using Dukkantek.Inventory.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dukkantek.Inventory.Persistence.Products.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            builder.Ignore(p => p.DomainEvents);

            builder.Property(p => p.RowVersion)
                .IsRowVersion();
        }
    }
}
