using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class AddColumnRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleOfUser",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 23, 15, 3, 1, 346, DateTimeKind.Local).AddTicks(9944));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 23, 15, 3, 1, 352, DateTimeKind.Local).AddTicks(778));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleOfUser",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 16, 23, 38, 37, 913, DateTimeKind.Local).AddTicks(6391));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 16, 23, 38, 37, 921, DateTimeKind.Local).AddTicks(3337));
        }
    }
}
