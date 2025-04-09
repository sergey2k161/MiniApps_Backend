using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniApps_Backend.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class INIT17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Transactions");
        }
    }
}
