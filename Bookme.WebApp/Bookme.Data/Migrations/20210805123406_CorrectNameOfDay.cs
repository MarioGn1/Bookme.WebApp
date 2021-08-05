using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookme.Data.Migrations
{
    public partial class CorrectNameOfDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tuesdey",
                table: "WeeklySchedules",
                newName: "Tuesday");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tuesday",
                table: "WeeklySchedules",
                newName: "Tuesdey");
        }
    }
}
