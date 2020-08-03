using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class ModifyClassDefect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfReport",
                table: "Defects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Defects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationNumber",
                table: "Defects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Defects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
              name: "Phone",
              table: "Defects",
              nullable: true);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 2, 15, 31, 26, 559, DateTimeKind.Local).AddTicks(458));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 2, 15, 31, 26, 564, DateTimeKind.Local).AddTicks(2984));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                columns: new[] { "DateCreated", "RentalDate", "ReturnDate" },
                values: new object[] { new DateTime(2020, 8, 2, 15, 31, 26, 564, DateTimeKind.Local).AddTicks(1012), new DateTime(2020, 8, 4, 15, 31, 26, 563, DateTimeKind.Local).AddTicks(9528), new DateTime(2020, 8, 7, 15, 31, 26, 563, DateTimeKind.Local).AddTicks(9949) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 2, 15, 31, 26, 563, DateTimeKind.Local).AddTicks(8281));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfReport",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Defects");

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
    }
}
