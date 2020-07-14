using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class AddNewDataCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfDoor",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSits",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfCar",
                table: "Cars",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 17, 35, 3, 732, DateTimeKind.Local).AddTicks(6474));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 17, 35, 3, 739, DateTimeKind.Local).AddTicks(2704));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 17, 35, 3, 739, DateTimeKind.Local).AddTicks(2864));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDoor",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "NumberOfSits",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TypeOfCar",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 17, 34, 22, 484, DateTimeKind.Local).AddTicks(5496));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 17, 34, 22, 494, DateTimeKind.Local).AddTicks(773));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2020, 7, 10, 17, 34, 22, 494, DateTimeKind.Local).AddTicks(1029));
        }
    }
}
