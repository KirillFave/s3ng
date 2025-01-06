using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Migrations
{
    /// <inheritdoc />
    public partial class ModelsModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Delivery_Time",
                table: "Deliveries",
                newName: "EstimatedDeliveryTime");

            migrationBuilder.RenameColumn(
                name: "CreateTimeestamp",
                table: "Deliveries",
                newName: "CreateTimestamp");

            migrationBuilder.RenameColumn(
                name: "Courer_Id",
                table: "Deliveries",
                newName: "CourierId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualDeliveryTime",
                table: "Deliveries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeliveryStatus",
                table: "Deliveries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Deliveries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "Deliveries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Couriers_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalQuantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PaymentType = table.Column<int>(type: "integer", nullable: false),
                    ShippingAddress = table.Column<string>(type: "text", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeliveryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_DeliveryId",
                table: "Couriers",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_DeliveryId",
                table: "OrderItem",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Couriers");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropColumn(
                name: "ActualDeliveryTime",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Deliveries");

            migrationBuilder.RenameColumn(
                name: "EstimatedDeliveryTime",
                table: "Deliveries",
                newName: "Delivery_Time");

            migrationBuilder.RenameColumn(
                name: "CreateTimestamp",
                table: "Deliveries",
                newName: "CreateTimeestamp");

            migrationBuilder.RenameColumn(
                name: "CourierId",
                table: "Deliveries",
                newName: "Courer_Id");
        }
    }
}
