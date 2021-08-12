using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookme.Data.Migrations
{
    public partial class ChangedBusinessTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessInfos_AspNetUsers_UserId",
                table: "BusinessInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessInfos_BookingConfigurations_BookingConfigurationId",
                table: "BusinessInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BusinessInfos_BusinessInfoId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessInfos",
                table: "BusinessInfos");

            migrationBuilder.RenameTable(
                name: "BusinessInfos",
                newName: "Businesses");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessInfos_UserId",
                table: "Businesses",
                newName: "IX_Businesses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessInfos_BookingConfigurationId",
                table: "Businesses",
                newName: "IX_Businesses_BookingConfigurationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Businesses",
                table: "Businesses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_AspNetUsers_UserId",
                table: "Businesses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BookingConfigurations_BookingConfigurationId",
                table: "Businesses",
                column: "BookingConfigurationId",
                principalTable: "BookingConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Businesses_BusinessInfoId",
                table: "Comments",
                column: "BusinessInfoId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_AspNetUsers_UserId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BookingConfigurations_BookingConfigurationId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Businesses_BusinessInfoId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Businesses",
                table: "Businesses");

            migrationBuilder.RenameTable(
                name: "Businesses",
                newName: "BusinessInfos");

            migrationBuilder.RenameIndex(
                name: "IX_Businesses_UserId",
                table: "BusinessInfos",
                newName: "IX_BusinessInfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Businesses_BookingConfigurationId",
                table: "BusinessInfos",
                newName: "IX_BusinessInfos_BookingConfigurationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessInfos",
                table: "BusinessInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessInfos_AspNetUsers_UserId",
                table: "BusinessInfos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessInfos_BookingConfigurations_BookingConfigurationId",
                table: "BusinessInfos",
                column: "BookingConfigurationId",
                principalTable: "BookingConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BusinessInfos_BusinessInfoId",
                table: "Comments",
                column: "BusinessInfoId",
                principalTable: "BusinessInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
