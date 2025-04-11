using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniApps_Backend.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class INIT29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubscriptions_Users_UserId",
                table: "CourseSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSubscriptions",
                table: "CourseSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CourseSubscriptions_UserId",
                table: "CourseSubscriptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseSubscriptions");

            migrationBuilder.AddColumn<long>(
                name: "TelegramId",
                table: "CourseSubscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_TelegramId",
                table: "Users",
                column: "TelegramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSubscriptions",
                table: "CourseSubscriptions",
                columns: new[] { "CourseId", "TelegramId" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubscriptions_TelegramId",
                table: "CourseSubscriptions",
                column: "TelegramId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubscriptions_Users_TelegramId",
                table: "CourseSubscriptions",
                column: "TelegramId",
                principalTable: "Users",
                principalColumn: "TelegramId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubscriptions_Users_TelegramId",
                table: "CourseSubscriptions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_TelegramId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSubscriptions",
                table: "CourseSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CourseSubscriptions_TelegramId",
                table: "CourseSubscriptions");

            migrationBuilder.DropColumn(
                name: "TelegramId",
                table: "CourseSubscriptions");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CourseSubscriptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSubscriptions",
                table: "CourseSubscriptions",
                columns: new[] { "CourseId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubscriptions_UserId",
                table: "CourseSubscriptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubscriptions_Users_UserId",
                table: "CourseSubscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
