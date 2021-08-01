using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookme.Data.Migrations
{
    public partial class EditBookingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ConfirmationTypes_ConfirmationId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "ConfirmationId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ClientFirstName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientLastName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientPhoneNumber",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ConfirmationTypes_ConfirmationId",
                table: "Bookings",
                column: "ConfirmationId",
                principalTable: "ConfirmationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ConfirmationTypes_ConfirmationId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ClientFirstName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ClientLastName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ClientPhoneNumber",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "ConfirmationId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ConfirmationTypes_ConfirmationId",
                table: "Bookings",
                column: "ConfirmationId",
                principalTable: "ConfirmationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
