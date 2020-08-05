using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class addColumnIsDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 4, 13, 12, 2, 516, DateTimeKind.Local).AddTicks(1838));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 4, 13, 12, 2, 522, DateTimeKind.Local).AddTicks(8392));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                columns: new[] { "DateCreated", "RentalDate", "ReturnDate" },
                values: new object[] { new DateTime(2020, 8, 4, 13, 12, 2, 522, DateTimeKind.Local).AddTicks(4193), new DateTime(2020, 8, 6, 13, 12, 2, 522, DateTimeKind.Local).AddTicks(2170), new DateTime(2020, 8, 9, 13, 12, 2, 522, DateTimeKind.Local).AddTicks(2678) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 4, 13, 12, 2, 522, DateTimeKind.Local).AddTicks(911));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 3, 13, 29, 42, 951, DateTimeKind.Local).AddTicks(435));

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
