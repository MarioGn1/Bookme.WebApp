using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookme.Data.Migrations
{
    public partial class AddImageUrlForOfferedService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "OfferedServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "OfferedServices");
        }
    }
}
