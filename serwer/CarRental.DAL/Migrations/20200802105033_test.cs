using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 2, 12, 50, 33, 316, DateTimeKind.Local).AddTicks(3431));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 2, 12, 50, 33, 321, DateTimeKind.Local).AddTicks(8636));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                columns: new[] { "DateCreated", "RentalDate", "ReturnDate" },
                values: new object[] { new DateTime(2020, 8, 2, 12, 50, 33, 321, DateTimeKind.Local).AddTicks(4973), new DateTime(2020, 8, 4, 12, 50, 33, 321, DateTimeKind.Local).AddTicks(2334), new DateTime(2020, 8, 7, 12, 50, 33, 321, DateTimeKind.Local).AddTicks(2755) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 2, 12, 50, 33, 321, DateTimeKind.Local).AddTicks(1270));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 27, 19, 21, 47, 347, DateTimeKind.Local).AddTicks(5344));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 27, 19, 21, 47, 352, DateTimeKind.Local).AddTicks(5023));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                columns: new[] { "DateCreated", "RentalDate", "ReturnDate" },
                values: new object[] { new DateTime(2020, 7, 27, 19, 21, 47, 352, DateTimeKind.Local).AddTicks(3404), new DateTime(2020, 7, 29, 19, 21, 47, 352, DateTimeKind.Local).AddTicks(2206), new DateTime(2020, 8, 1, 19, 21, 47, 352, DateTimeKind.Local).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 30, 18, 9, 9, 850, DateTimeKind.Local).AddTicks(7695));
        }
    }
}
