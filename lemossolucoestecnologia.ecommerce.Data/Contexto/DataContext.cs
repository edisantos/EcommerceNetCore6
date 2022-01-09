using lemossolucoestecnologia.ecommerce.Data.DataConfig;
using lemossolucoestecnologia.ecommerce.Domain.Entities;
using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lemossolucoestecnologia.ecommerce.Data.Contexto
{
    public class DataContext : IdentityDbContext<Users>
    {
        public DbSet<Users>? Users { get; set; }
        public DbSet<Products>? Products { get; set; }
        public DbSet<Sales>? Sales { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>(new UserConfiguration().Configure);
            builder.Entity<Products>(new ProductsConfiguration().Configure);
            builder.Entity<Sales>(new SalesConfiguration().Configure);
            base.OnModelCreating(builder);
        }
    }
}
