using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class CarIsDeleteAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cars",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 4, 14, 7, 16, 776, DateTimeKind.Local).AddTicks(1379));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 4, 14, 7, 16, 783, DateTimeKind.Local).AddTicks(3789));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1,
                columns: new[] { "DateCreated", "RentalDate", "ReturnDate" },
                values: new object[] { new DateTime(2020, 8, 4, 14, 7, 16, 783, DateTimeKind.Local).AddTicks(657), new DateTime(2020, 8, 6, 14, 7, 16, 782, DateTimeKind.Local).AddTicks(9217), new DateTime(2020, 8, 9, 14, 7, 16, 782, DateTimeKind.Local).AddTicks(9650) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 4, 14, 7, 16, 782, DateTimeKind.Local).AddTicks(7739));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 3, 14, 38, 35, 147, DateTimeKind.Local).AddTicks(5896));

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
    }
}
