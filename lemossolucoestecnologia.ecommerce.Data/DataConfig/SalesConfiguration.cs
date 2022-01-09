using lemossolucoestecnologia.ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lemossolucoestecnologia.ecommerce.Data.DataConfig
{
    public class SalesConfiguration : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(k => k.Id);

            builder.Property(k => k.Id).ValueGeneratedOnAdd();


            builder.HasOne(p => p.Products)
                .WithMany(s => s.Sales)
                .HasForeignKey(p => p.ProductId)
                .IsRequired();

            builder.HasOne(us => us.Users)
                .WithMany(s => s.Sales)
                .HasForeignKey(us => us.UserId)
                .IsRequired();


        }
    }
}
