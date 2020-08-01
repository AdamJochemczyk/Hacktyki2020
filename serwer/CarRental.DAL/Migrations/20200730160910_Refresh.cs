using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class Refresh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Cars",
            //    columns: table => new
            //    {
            //        CarId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DateCreated = table.Column<DateTime>(nullable: false),
            //        DateModified = table.Column<DateTime>(nullable: false),
            //        ModifiedBy = table.Column<string>(nullable: true),
            //        Brand = table.Column<string>(nullable: true),
            //        RegistrationNumber = table.Column<string>(nullable: true),
            //        Model = table.Column<string>(nullable: true),
            //        TypeOfCar = table.Column<int>(nullable: false),
            //        NumberOfDoor = table.Column<int>(nullable: false),
            //        NumberOfSits = table.Column<int>(nullable: false),
            //        YearOfProduction = table.Column<int>(nullable: false),
            //        ImagePath = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Cars", x => x.CarId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DateCreated = table.Column<DateTime>(nullable: false),
            //        DateModified = table.Column<DateTime>(nullable: false),
            //        ModifiedBy = table.Column<string>(nullable: true),
            //        FirstName = table.Column<string>(nullable: true),
            //        LastName = table.Column<string>(nullable: true),
            //        NumberIdentificate = table.Column<string>(nullable: true),
            //        Email = table.Column<string>(nullable: true),
            //        MobileNumber = table.Column<string>(nullable: true),
            //        HashPassword = table.Column<string>(nullable: true),
            //        Salt = table.Column<string>(nullable: true),
            //        StatusOfVerification = table.Column<string>(nullable: true),
            //        RoleOfUser = table.Column<int>(nullable: false),
            //        CodeOfVerification = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.UserId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Locations",
            //    columns: table => new
            //    {
            //        LocationId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DateCreated = table.Column<DateTime>(nullable: false),
            //        DateModified = table.Column<DateTime>(nullable: false),
            //        ModifiedBy = table.Column<string>(nullable: true),
            //        CarId = table.Column<int>(nullable: false),
            //        Position = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Locations", x => x.LocationId);
            //        table.ForeignKey(
            //            name: "FK_Locations_Cars_CarId",
            //            column: x => x.CarId,
            //            principalTable: "Cars",
            //            principalColumn: "CarId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Defects",
            //    columns: table => new
            //    {
            //        DefectId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DateCreated = table.Column<DateTime>(nullable: false),
            //        DateModified = table.Column<DateTime>(nullable: false),
            //        ModifiedBy = table.Column<string>(nullable: true),
            //        Description = table.Column<string>(nullable: true),
            //        UserId = table.Column<int>(nullable: false),
            //        CarId = table.Column<int>(nullable: false),
            //        Status = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Defects", x => x.DefectId);
            //        table.ForeignKey(
            //            name: "FK_Defects_Cars_CarId",
            //            column: x => x.CarId,
            //            principalTable: "Cars",
            //            principalColumn: "CarId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Defects_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "UserId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "Refresh",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Refresh = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    DateOfStart = table.Column<DateTime>(nullable: false),
                    DateOfEnd = table.Column<DateTime>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refresh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Refresh_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    RentalDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    CarId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Brand", "DateCreated", "DateModified", "ImagePath", "Model", "ModifiedBy", "NumberOfDoor", "NumberOfSits", "RegistrationNumber", "TypeOfCar", "YearOfProduction" },
                values: new object[] { 1, "Audi", new DateTime(2020, 7, 30, 18, 9, 9, 844, DateTimeKind.Local).AddTicks(6799), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://pngimg.com/uploads/audi/audi_PNG1737.png", "Q5", null, 0, 0, null, 0, 2019 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CodeOfVerification", "DateCreated", "DateModified", "Email", "FirstName", "HashPassword", "LastName", "MobileNumber", "ModifiedBy", "NumberIdentificate", "RoleOfUser", "Salt", "StatusOfVerification" },
                values: new object[] { 1, null, new DateTime(2020, 7, 30, 18, 9, 9, 850, DateTimeKind.Local).AddTicks(7695), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "John", null, "Doe", null, null, null, 0, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Defects_CarId",
                table: "Defects",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_UserId",
                table: "Defects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CarId",
                table: "Locations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Refresh_UserId",
                table: "Refresh",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Refresh");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
