using LivrariaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrariaApi.Data.Mapping;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .ToTable("Customer");
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id);
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);
        builder
            .HasIndex(x => x.Id, "IX_Book_Id");
        builder
            .HasMany(x => x.Orders)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .HasConstraintName("FK_OrderItem_Order")
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.User)
            .WithOne(x => x.customer)
            .HasForeignKey<Customer>(x => x.UserId)
            .HasConstraintName("FK_UserCustomer_User")
            .OnDelete(DeleteBehavior.Cascade);
    }
}