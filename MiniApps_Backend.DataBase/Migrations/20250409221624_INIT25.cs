using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniApps_Backend.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class INIT25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubscriptions_Courses_CourseId1",
                table: "CourseSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubscriptions_Users_UserId1",
                table: "CourseSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CourseSubscriptions_CourseId1",
                table: "CourseSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CourseSubscriptions_UserId1",
                table: "CourseSubscriptions");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "CourseSubscriptions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CourseSubscriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseId1",
                table: "CourseSubscriptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "CourseSubscriptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubscriptions_CourseId1",
                table: "CourseSubscriptions",
                column: "CourseId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubscriptions_UserId1",
                table: "CourseSubscriptions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubscriptions_Courses_CourseId1",
                table: "CourseSubscriptions",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubscriptions_Users_UserId1",
                table: "CourseSubscriptions",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
