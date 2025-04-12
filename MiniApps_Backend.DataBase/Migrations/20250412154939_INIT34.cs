using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniApps_Backend.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class INIT34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    LessonId = table.Column<Guid>(type: "uuid", nullable: false),
                    WhenСompleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    TestId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastTry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Result = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonResults");

            migrationBuilder.DropTable(
                name: "TestResults");
        }
    }
}
