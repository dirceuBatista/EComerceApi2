using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaApi.Migrations
{
    /// <inheritdoc />
    public partial class segmigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoriId",
                table: "BookCategory",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCategory_CategoriId",
                table: "BookCategory",
                newName: "IX_BookCategory_CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "BookCategory",
                newName: "CategoriId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCategory_CategoryId",
                table: "BookCategory",
                newName: "IX_BookCategory_CategoriId");
        }
    }
}
