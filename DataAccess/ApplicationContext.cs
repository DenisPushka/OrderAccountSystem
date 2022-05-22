using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationContext()
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public Product[] CreateTableProducts() =>
            new[]
            {
                new Product {Count = 15, Name = "Apple", Price = 120, IsStock = true},
                new Product {Count = 20, Name = "Pear", Price = 180, IsStock = true},
                new Product {Count = 25, Name = "Orange", Price = 300, IsStock = true}
            };

        public Client[] CreateTableClients() =>
            new[]
            {
                new Client {Name = "Denis"},
                new Client {Name = "Sergey"},
                new Client {Name = "Gregory"}
            };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=OrderAccountSystem;Username=postgres;Password=ogr84Bqk3");
    }
}