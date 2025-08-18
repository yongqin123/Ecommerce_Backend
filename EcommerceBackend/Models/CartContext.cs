using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace EcommerceBackend.Models
{
    public class CartContext : DbContext
    {
        protected readonly IConfiguration configuration;
        public CartContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Cart> Carts { get; set; }
    }
}
