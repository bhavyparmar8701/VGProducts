using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGProducts.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Review2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Product",
                newName: "Reating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reating",
                table: "Product",
                newName: "Rating");
        }
    }
}
