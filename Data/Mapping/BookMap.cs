using LivrariaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrariaApi.Data.Mapping;

public class BookMap : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .ToTable("Book");
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x=>x.Id);
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);
        builder
            .Property(x => x.Author)
            .HasColumnName("Author")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);
        builder
            .Property(x => x.Slug)
            .IsRequired()
            .HasColumnName("Slug")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);
        builder
            .HasIndex(x => x.Id, "IX_Book_Id");
        builder
            .HasMany(x => x.Category)
            .WithMany(x => x.Books)
            .UsingEntity<Dictionary<string, object>>(
                "BookCategory",
                category => category
                    .HasOne<Category>()
                    .WithMany()
                    .HasForeignKey("CategoryId")
                    .HasConstraintName("FK_BookCategory_CategoryId")
                    .OnDelete(DeleteBehavior.Cascade),
                book => book
                    .HasOne<Book>()
                    .WithMany()
                    .HasForeignKey("BookId")
                    .HasConstraintName("FK_BookCategory_BookId")
                    .OnDelete(DeleteBehavior.Cascade));
        
    }
}