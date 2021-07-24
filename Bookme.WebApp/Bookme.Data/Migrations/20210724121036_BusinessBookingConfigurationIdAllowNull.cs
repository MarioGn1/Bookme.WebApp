using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookme.Data.Migrations
{
    public partial class BusinessBookingConfigurationIdAllowNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessInfos_BookingConfigurations_BookingConfigurationId",
                table: "BusinessInfos");

            migrationBuilder.DropIndex(
                name: "IX_BusinessInfos_BookingConfigurationId",
                table: "BusinessInfos");

            migrationBuilder.AlterColumn<int>(
                name: "BookingConfigurationId",
                table: "BusinessInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInfos_BookingConfigurationId",
                table: "BusinessInfos",
                column: "BookingConfigurationId",
                unique: true,
                filter: "[BookingConfigurationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessInfos_BookingConfigurations_BookingConfigurationId",
                table: "BusinessInfos",
                column: "BookingConfigurationId",
                principalTable: "BookingConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessInfos_BookingConfigurations_BookingConfigurationId",
                table: "BusinessInfos");

            migrationBuilder.DropIndex(
                name: "IX_BusinessInfos_BookingConfigurationId",
                table: "BusinessInfos");

            migrationBuilder.AlterColumn<int>(
                name: "BookingConfigurationId",
                table: "BusinessInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInfos_BookingConfigurationId",
                table: "BusinessInfos",
                column: "BookingConfigurationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessInfos_BookingConfigurations_BookingConfigurationId",
                table: "BusinessInfos",
                column: "BookingConfigurationId",
                principalTable: "BookingConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
