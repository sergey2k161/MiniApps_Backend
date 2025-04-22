using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniApps_Backend.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class INIT48 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSucsessDto",
                table: "CourseSucsessDto");

            migrationBuilder.RenameTable(
                name: "CourseSucsessDto",
                newName: "CourseResults");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseResults",
                table: "CourseResults",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BlocksSucsess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    BlockId = table.Column<Guid>(type: "uuid", nullable: false),
                    FinishAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Finish = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlocksSucsess", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlocksSucsess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseResults",
                table: "CourseResults");

            migrationBuilder.RenameTable(
                name: "CourseResults",
                newName: "CourseSucsessDto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSucsessDto",
                table: "CourseSucsessDto",
                column: "Id");
        }
    }
}
