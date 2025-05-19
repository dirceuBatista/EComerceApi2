using LivrariaApi.Data.Mapping;
using LivrariaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new OrderMap());
        modelBuilder.ApplyConfiguration(new OrderItemMap());
        modelBuilder.ApplyConfiguration(new CustomerMap());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        var conection = builder.UseSqlServer
            ("server=localhost,1433; DataBase=Livraria;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;");
    }


}