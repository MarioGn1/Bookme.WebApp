using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookme.Data.Migrations
{
    public partial class BusinessPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "BusinessInfos");

            migrationBuilder.RenameColumn(
                name: "SupportedLocationArea",
                table: "BusinessInfos",
                newName: "BusinessName");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "BusinessInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "BusinessInfos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SupportedLocation",
                table: "BusinessInfos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "BusinessInfos");

            migrationBuilder.DropColumn(
                name: "SupportedLocation",
                table: "BusinessInfos");

            migrationBuilder.RenameColumn(
                name: "BusinessName",
                table: "BusinessInfos",
                newName: "SupportedLocationArea");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "BusinessInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "BusinessInfos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
