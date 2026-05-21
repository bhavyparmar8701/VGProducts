using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGProducts.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FullFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reating",
                table: "Product",
                newName: "Reting");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewCount",
                table: "Product",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reting",
                table: "Product",
                newName: "Reating");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewCount",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
