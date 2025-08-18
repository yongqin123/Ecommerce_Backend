using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;


namespace EcommerceBackend.Models
{
    public class ItemContext : DbContext
    {
        protected readonly IConfiguration configuration;
        public ItemContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        }

        public DbSet<Item> Items { get; set; }
    }
}
