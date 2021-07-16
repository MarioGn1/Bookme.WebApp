using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookme.Data.Migrations
{
    public partial class ConnectOfferedServicesToServiceCategor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferedServices_AspNetUsers_BusinessId",
                table: "OfferedServices");

            migrationBuilder.DropIndex(
                name: "IX_OfferedServices_BusinessId",
                table: "OfferedServices");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "OfferedServices");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "OfferedServices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceCategoryId",
                table: "OfferedServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OfferedServices_ApplicationUserId",
                table: "OfferedServices",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedServices_ServiceCategoryId",
                table: "OfferedServices",
                column: "ServiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedServices_AspNetUsers_ApplicationUserId",
                table: "OfferedServices",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedServices_ServiceCategories_ServiceCategoryId",
                table: "OfferedServices",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferedServices_AspNetUsers_ApplicationUserId",
                table: "OfferedServices");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedServices_ServiceCategories_ServiceCategoryId",
                table: "OfferedServices");

            migrationBuilder.DropIndex(
                name: "IX_OfferedServices_ApplicationUserId",
                table: "OfferedServices");

            migrationBuilder.DropIndex(
                name: "IX_OfferedServices_ServiceCategoryId",
                table: "OfferedServices");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "OfferedServices");

            migrationBuilder.DropColumn(
                name: "ServiceCategoryId",
                table: "OfferedServices");

            migrationBuilder.AddColumn<string>(
                name: "BusinessId",
                table: "OfferedServices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedServices_BusinessId",
                table: "OfferedServices",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedServices_AspNetUsers_BusinessId",
                table: "OfferedServices",
                column: "BusinessId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
