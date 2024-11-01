using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdersChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Services_ServiceId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOption_Services_ServiceId",
                table: "ServiceOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceOption",
                table: "ServiceOption");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "ServiceOption",
                newName: "ServiceOptions");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Orders",
                newName: "ServiceOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ServiceId",
                table: "Orders",
                newName: "IX_Orders_ServiceOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceOption_ServiceId",
                table: "ServiceOptions",
                newName: "IX_ServiceOptions_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceOptions",
                table: "ServiceOptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ServiceOptions_ServiceOptionId",
                table: "Orders",
                column: "ServiceOptionId",
                principalTable: "ServiceOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOptions_Services_ServiceId",
                table: "ServiceOptions",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ServiceOptions_ServiceOptionId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOptions_Services_ServiceId",
                table: "ServiceOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceOptions",
                table: "ServiceOptions");

            migrationBuilder.RenameTable(
                name: "ServiceOptions",
                newName: "ServiceOption");

            migrationBuilder.RenameColumn(
                name: "ServiceOptionId",
                table: "Orders",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ServiceOptionId",
                table: "Orders",
                newName: "IX_Orders_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceOptions_ServiceId",
                table: "ServiceOption",
                newName: "IX_ServiceOption_ServiceId");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Services",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceOption",
                table: "ServiceOption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Services_ServiceId",
                table: "Orders",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOption_Services_ServiceId",
                table: "ServiceOption",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
