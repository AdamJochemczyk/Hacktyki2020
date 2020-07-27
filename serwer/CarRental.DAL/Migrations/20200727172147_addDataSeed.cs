using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class addDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 27, 19, 21, 47, 347, DateTimeKind.Local).AddTicks(5344));

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "CarId", "DateCreated", "DateModified", "IsFinished", "ModifiedBy", "RentalDate", "ReturnDate", "UserId" },
                values: new object[] { 1, 1, new DateTime(2020, 7, 27, 19, 21, 47, 352, DateTimeKind.Local).AddTicks(3404), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2020, 7, 29, 19, 21, 47, 352, DateTimeKind.Local).AddTicks(2206), new DateTime(2020, 8, 1, 19, 21, 47, 352, DateTimeKind.Local).AddTicks(2540), 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 27, 19, 21, 47, 352, DateTimeKind.Local).AddTicks(1347));

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "DateCreated", "DateModified", "IsActual", "Latitude", "Longitude", "ModifiedBy", "ReservationId" },
                values: new object[] { 1, new DateTime(2020, 7, 27, 19, 21, 47, 352, DateTimeKind.Local).AddTicks(5023), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 50.5, 43.299999999999997, null, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 27, 19, 20, 51, 632, DateTimeKind.Local).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 27, 19, 20, 51, 637, DateTimeKind.Local).AddTicks(5194));
        }
    }
}
