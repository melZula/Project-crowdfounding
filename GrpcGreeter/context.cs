using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace GrpcGreeter
{
    public class ApplicationContext : DbContext
    {
        private string connectionString;
        public DbSet<User> Users { get; set; }

        public DbSet<Found> Founds { get; set; }

        public ApplicationContext(string _connectionString)
        {
            connectionString = _connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}