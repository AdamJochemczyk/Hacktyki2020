using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class ChangesInDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                columns: new[] { "DateCreated", "NumberOfDoor", "NumberOfSits", "RegistrationNumber", "TypeOfCar" },
                values: new object[] { new DateTime(2020, 8, 3, 14, 38, 35, 147, DateTimeKind.Local).AddTicks(5896), 5, 5, "SZE4562", 1 });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 3, 14, 38, 35, 152, DateTimeKind.Local).AddTicks(7022));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                columns: new[] { "DateCreated", "RentalDate", "ReturnDate" },
                values: new object[] { new DateTime(2020, 8, 3, 14, 38, 35, 152, DateTimeKind.Local).AddTicks(5199), new DateTime(2020, 8, 5, 14, 38, 35, 152, DateTimeKind.Local).AddTicks(3723), new DateTime(2020, 8, 8, 14, 38, 35, 152, DateTimeKind.Local).AddTicks(4185) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 3, 14, 38, 35, 152, DateTimeKind.Local).AddTicks(2314));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                columns: new[] { "DateCreated", "NumberOfDoor", "NumberOfSits", "RegistrationNumber", "TypeOfCar" },
                values: new object[] { new DateTime(2020, 8, 3, 13, 29, 42, 951, DateTimeKind.Local).AddTicks(435), 0, 0, null, 0 });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 3, 13, 29, 42, 959, DateTimeKind.Local).AddTicks(836));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                columns: new[] { "DateCreated", "RentalDate", "ReturnDate" },
                values: new object[] { new DateTime(2020, 8, 3, 13, 29, 42, 958, DateTimeKind.Local).AddTicks(7638), new DateTime(2020, 8, 5, 13, 29, 42, 958, DateTimeKind.Local).AddTicks(5022), new DateTime(2020, 8, 8, 13, 29, 42, 958, DateTimeKind.Local).AddTicks(5710) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 3, 13, 29, 42, 958, DateTimeKind.Local).AddTicks(3295));
        }
    }
}
