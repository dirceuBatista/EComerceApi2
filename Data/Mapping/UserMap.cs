using LivrariaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrariaApi.Data.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("User");
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
            .Property(x => x.PasswordHash)
            .HasColumnName("Password")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);
        builder
            .Property(x => x.Slug)
            .HasColumnName("Slug")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);
        builder
            .HasIndex(x => x.Email, "IX_User_Email")
            .IsUnique();
        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role => role
            
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRole_RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                user => user
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRole_UserId")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}