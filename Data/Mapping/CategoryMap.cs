using LivrariaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrariaApi.Data.Mapping;

public class CategoryMap :IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable("Category");
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id);
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        builder
            .Property(x => x.Slug)
            .IsRequired()
            .HasColumnName("Slug")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);
        builder
            .HasIndex(x => x.Id, "IX_Book_Id");
    }
}