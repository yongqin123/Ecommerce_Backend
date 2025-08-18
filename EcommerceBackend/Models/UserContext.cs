using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace EcommerceBackend.Models
{
    public class UserContext : DbContext
    {
        protected readonly IConfiguration configuration;
        public UserContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        }

        public DbSet<User> Users { get; set; }
    }
}
