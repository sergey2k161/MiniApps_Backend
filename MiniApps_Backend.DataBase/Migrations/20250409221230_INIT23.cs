using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniApps_Backend.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class INIT23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseSubscriptions",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSubscriptions", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CourseSubscriptions_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSubscriptions_Courses_CourseId1",
                        column: x => x.CourseId1,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseSubscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSubscriptions_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubscriptions_CourseId1",
                table: "CourseSubscriptions",
                column: "CourseId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubscriptions_UserId",
                table: "CourseSubscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubscriptions_UserId1",
                table: "CourseSubscriptions",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseSubscriptions");
        }
    }
}
