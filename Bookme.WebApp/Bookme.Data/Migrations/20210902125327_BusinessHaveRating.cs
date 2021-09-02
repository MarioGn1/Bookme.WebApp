using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookme.Data.Migrations
{
    public partial class BusinessHaveRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raitings_AspNetUsers_VoterId",
                table: "Raitings");

            migrationBuilder.DropForeignKey(
                name: "FK_Raitings_Comments_CommentId",
                table: "Raitings");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Raitings",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Raitings_CommentId",
                table: "Raitings",
                newName: "IX_Raitings_BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Raitings_AspNetUsers_VoterId",
                table: "Raitings",
                column: "VoterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Raitings_Businesses_BusinessId",
                table: "Raitings",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raitings_AspNetUsers_VoterId",
                table: "Raitings");

            migrationBuilder.DropForeignKey(
                name: "FK_Raitings_Businesses_BusinessId",
                table: "Raitings");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Raitings",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Raitings_BusinessId",
                table: "Raitings",
                newName: "IX_Raitings_CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Raitings_AspNetUsers_VoterId",
                table: "Raitings",
                column: "VoterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Raitings_Comments_CommentId",
                table: "Raitings",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
