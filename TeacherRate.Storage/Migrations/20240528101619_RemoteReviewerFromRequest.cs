using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherRate.Storage.Migrations
{
    /// <inheritdoc />
    public partial class RemoteReviewerFromRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherRequest_User_ReviewerId",
                table: "TeacherRequest");

            migrationBuilder.DropIndex(
                name: "IX_TeacherRequest_ReviewerId",
                table: "TeacherRequest");

            migrationBuilder.RenameColumn(
                name: "ReviewerId",
                table: "TeacherRequest",
                newName: "Points");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Points",
                table: "TeacherRequest",
                newName: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherRequest_ReviewerId",
                table: "TeacherRequest",
                column: "ReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherRequest_User_ReviewerId",
                table: "TeacherRequest",
                column: "ReviewerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
