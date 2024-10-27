using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EpalProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileType",
                table: "Users",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileType",
                table: "Users");
        }
    }
}
