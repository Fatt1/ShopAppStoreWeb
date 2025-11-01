using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAppStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "AppImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "App",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "AppImage");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "App");
        }
    }
}
