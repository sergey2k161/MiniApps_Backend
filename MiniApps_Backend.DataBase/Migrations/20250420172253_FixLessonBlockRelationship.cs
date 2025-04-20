using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniApps_Backend.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class FixLessonBlockRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Tests_TestId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TestId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Tests",
                newName: "BlockId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Lessons",
                newName: "BlockId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                newName: "IX_Lessons_BlockId");

            migrationBuilder.AddColumn<Guid>(
                name: "BlockId",
                table: "Courses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TestId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blocks_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blocks_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_CourseId",
                table: "Blocks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_TestId",
                table: "Blocks",
                column: "TestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Blocks_BlockId",
                table: "Lessons",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Blocks_BlockId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "BlockId",
                table: "Tests",
                newName: "LessonId");

            migrationBuilder.RenameColumn(
                name: "BlockId",
                table: "Lessons",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_BlockId",
                table: "Lessons",
                newName: "IX_Lessons_CourseId");

            migrationBuilder.AddColumn<Guid>(
                name: "TestId",
                table: "Lessons",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TestId",
                table: "Lessons",
                column: "TestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Tests_TestId",
                table: "Lessons",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
