using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class ChangeLocationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Cars_CarId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CarId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Locations");

            migrationBuilder.AddColumn<bool>(
                name: "IsActual",
                table: "Locations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Locations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Locations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Locations",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ReservationId",
                table: "Locations",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Reservations_ReservationId",
                table: "Locations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Reservations_ReservationId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_ReservationId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "IsActual",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Locations");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CarId",
                table: "Locations",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Cars_CarId",
                table: "Locations",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
