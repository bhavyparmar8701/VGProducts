using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGProducts.Repository.Migrations
{
    /// <inheritdoc />
    public partial class VerifyPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OtpExpiry",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResetOtp",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtpExpiry",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetOtp",
                table: "AspNetUsers");
        }
    }
}
