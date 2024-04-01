﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherRateProject.Migrations
{
    /// <inheritdoc />
    public partial class Task : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistredAt",
                table: "User",
                newName: "RegisteredAt");

            migrationBuilder.CreateTable(
                name: "TaskApprove",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskApprove", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatingTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    RatingPoints = table.Column<int>(type: "int", nullable: false),
                    RatingTip = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ApproveId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingTask_TaskApprove_ApproveId",
                        column: x => x.ApproveId,
                        principalTable: "TaskApprove",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingTask_TaskCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TaskCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ActualRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedTask_RatingTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "RatingTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedTask_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTask_TaskId",
                table: "CompletedTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTask_UserId",
                table: "CompletedTask",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingTask_ApproveId",
                table: "RatingTask",
                column: "ApproveId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingTask_CategoryId",
                table: "RatingTask",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedTask");

            migrationBuilder.DropTable(
                name: "RatingTask");

            migrationBuilder.DropTable(
                name: "TaskApprove");

            migrationBuilder.DropTable(
                name: "TaskCategory");

            migrationBuilder.RenameColumn(
                name: "RegisteredAt",
                table: "User",
                newName: "RegistredAt");
        }
    }
}
