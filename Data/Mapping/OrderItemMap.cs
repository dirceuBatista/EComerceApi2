using LivrariaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrariaApi.Data.Mapping;

public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder
            .ToTable("OrderItem");
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id);
        builder
            .Property(x => x.BookName)
            .IsRequired()
            .HasColumnName("NameBook")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        builder
            .Property(x => x.UnitPrice)
            .IsRequired()
            .HasColumnName("UnitPrice")
            .HasColumnType("decimal");
        builder
            .Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName("Quantity")
            .HasColumnType("int");
        builder
            .Property(x => x.Total)
            .HasColumnName("Total")
            .HasColumnType("decimal");
        builder
            .HasIndex(x => x.Id, "IX_Book_Id");
    }
}