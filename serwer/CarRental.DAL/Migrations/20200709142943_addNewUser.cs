using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class addNewUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 9, 16, 29, 42, 174, DateTimeKind.Local).AddTicks(3864));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 9, 16, 29, 42, 180, DateTimeKind.Local).AddTicks(2434));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateCreated", "DateModified", "FirstName", "LastName", "ModifiedBy" },
                values: new object[] { 2, new DateTime(2020, 7, 9, 16, 29, 42, 180, DateTimeKind.Local).AddTicks(2643), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "James", "Doe", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 8, 20, 19, 53, 642, DateTimeKind.Local).AddTicks(2414));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 8, 20, 19, 53, 648, DateTimeKind.Local).AddTicks(2762));
        }
    }
}
