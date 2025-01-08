using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TGU.Migrations
{
    /// <inheritdoc />
    public partial class AddCarPictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrls",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrls",
                table: "Cars");
        }
    }
}
