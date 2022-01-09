using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lemossolucoestecnologia.ecommerce.Data.DataConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(t => t.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.LastName)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.Property(x => x.Email)
               .HasColumnType("varchar(100)")
               .IsRequired();

            builder.Property(x => x.UserName)
               .HasColumnType("varchar(30)")
               .IsRequired();

            builder.Property(x => x.Address)
               .HasColumnType("varchar(500)")
               .IsRequired();

        }
    }
}
