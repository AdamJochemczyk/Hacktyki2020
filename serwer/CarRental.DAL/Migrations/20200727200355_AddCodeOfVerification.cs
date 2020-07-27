using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class AddCodeOfVerification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeOfVerification",
                table: "Users",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "Cars",
            //    keyColumn: "CarId",
            //    keyValue: 1,
            //    column: "DateCreated",
            //    value: new DateTime(2020, 7, 27, 22, 3, 54, 822, DateTimeKind.Local).AddTicks(271));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 27, 22, 3, 54, 832, DateTimeKind.Local).AddTicks(5856));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeOfVerification",
                table: "Users");

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
    }
}
