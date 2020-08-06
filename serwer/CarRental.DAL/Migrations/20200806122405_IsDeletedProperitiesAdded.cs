using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class IsDeletedProperitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 6, 14, 24, 4, 521, DateTimeKind.Local).AddTicks(3585));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 6, 14, 24, 4, 527, DateTimeKind.Local).AddTicks(620));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                columns: new[] { "DateCreated", "RentalDate", "ReturnDate" },
                values: new object[] { new DateTime(2020, 8, 6, 14, 24, 4, 526, DateTimeKind.Local).AddTicks(8559), new DateTime(2020, 8, 8, 14, 24, 4, 526, DateTimeKind.Local).AddTicks(6911), new DateTime(2020, 8, 11, 14, 24, 4, 526, DateTimeKind.Local).AddTicks(7338) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 6, 14, 24, 4, 526, DateTimeKind.Local).AddTicks(5845));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 6, 14, 21, 3, 315, DateTimeKind.Local).AddTicks(3483));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 6, 14, 21, 3, 327, DateTimeKind.Local).AddTicks(8243));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                columns: new[] { "DateCreated", "RentalDate", "ReturnDate" },
                values: new object[] { new DateTime(2020, 8, 6, 14, 21, 3, 327, DateTimeKind.Local).AddTicks(3697), new DateTime(2020, 8, 8, 14, 21, 3, 327, DateTimeKind.Local).AddTicks(119), new DateTime(2020, 8, 11, 14, 21, 3, 327, DateTimeKind.Local).AddTicks(1074) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 6, 14, 21, 3, 326, DateTimeKind.Local).AddTicks(7589));
        }
    }
}
