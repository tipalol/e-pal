using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "ProfileId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId1",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileId1",
                table: "Users",
                column: "ProfileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ProfileId1",
                table: "Users",
                column: "ProfileId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ProfileId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Users",
                newName: "Username");
        }
    }
}
