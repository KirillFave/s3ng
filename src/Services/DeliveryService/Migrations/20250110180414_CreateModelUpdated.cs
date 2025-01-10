using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Migrations
{
    /// <inheritdoc />
    public partial class CreateModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeModified",
                table: "Deliveries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeModified",
                table: "Deliveries");
        }
    }
}
