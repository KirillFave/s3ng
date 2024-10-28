using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "Guid", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: false),
                    Total_Price = table.Column<decimal>(type: "decimal", nullable: false),
                    Shipping_Address = table.Column<string>(type: "string", maxLength: 200, nullable: false),
                    Order_Id = table.Column<int>(type: "integer", nullable: false),
                    Courer_Id = table.Column<int>(type: "integer", nullable: false),
                    Delivery_Time = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delivery");
        }
    }
}
