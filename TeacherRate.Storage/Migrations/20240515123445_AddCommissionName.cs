using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherRate.Storage.Migrations
{
    /// <inheritdoc />
    public partial class AddCommissionName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommissionName",
                table: "User",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionName",
                table: "User");
        }
    }
}
