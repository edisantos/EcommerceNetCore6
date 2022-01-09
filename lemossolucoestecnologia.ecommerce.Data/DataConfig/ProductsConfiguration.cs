using lemossolucoestecnologia.ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lemossolucoestecnologia.ecommerce.Data.DataConfig
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(k => k.Id);
            builder.Property(k=>k.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ProductName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("varchar(300)")
                .IsRequired();

            
        }
    }
}
