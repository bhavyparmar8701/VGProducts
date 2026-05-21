using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGProducts.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Final1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingAmount",
                table: "Order",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldComputedColumnSql: "CASE WHEN \"TotalAmount\" >= 500 THEN 0 ELSE 50 END");

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "CartItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldComputedColumnSql: "\"Price\" * \"Quantity\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingAmount",
                table: "Order",
                type: "numeric",
                nullable: false,
                computedColumnSql: "CASE WHEN \"TotalAmount\" >= 500 THEN 0 ELSE 50 END",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "CartItems",
                type: "numeric",
                nullable: false,
                computedColumnSql: "\"Price\" * \"Quantity\"",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
