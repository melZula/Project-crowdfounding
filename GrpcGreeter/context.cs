using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Configuration;

namespace GrpcGreeter
{
    public class ApplicationContext : DbContext
    {
        private string connectionString;
        public DbSet<User> Users { get; set; }

        public DbSet<Found> Founds { get; set; }

        public ApplicationContext()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            connectionString = config["ConnectionStrings:foundb"];
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}