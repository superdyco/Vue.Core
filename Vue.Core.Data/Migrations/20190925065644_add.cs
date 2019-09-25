using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vue.Core.Data.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RefreshTimes",
                table: "UsersToken",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UsersToken",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTimes",
                table: "UsersToken");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UsersToken");
        }
    }
}
