using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAppStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColCombo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Combo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublicIdImage",
                table: "Combo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CategoryDescription",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Combo");

            migrationBuilder.DropColumn(
                name: "PublicIdImage",
                table: "Combo");

            migrationBuilder.DropColumn(
                name: "CategoryDescription",
                table: "Category");
        }
    }
}
