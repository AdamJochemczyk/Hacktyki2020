using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class EncodePassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EncodePassword",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 19, 3, 20, 77, DateTimeKind.Local).AddTicks(8752));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 19, 3, 20, 83, DateTimeKind.Local).AddTicks(7517));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 19, 3, 20, 83, DateTimeKind.Local).AddTicks(7672));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncodePassword",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 17, 35, 3, 732, DateTimeKind.Local).AddTicks(6474));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 17, 35, 3, 739, DateTimeKind.Local).AddTicks(2704));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 17, 35, 3, 739, DateTimeKind.Local).AddTicks(2864));
        }
    }
}
