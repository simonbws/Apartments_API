using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apartment_API.Migrations
{
    public partial class AddApartmentNumberToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApartmentNumbers",
                columns: table => new
                {
                    ApartmentNo = table.Column<int>(type: "int", nullable: false),
                    SpecialProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentNumbers", x => x.ApartmentNo);
                });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 10, 15, 19, 42, 665, DateTimeKind.Local).AddTicks(7472));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 10, 15, 19, 42, 665, DateTimeKind.Local).AddTicks(7507));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 10, 15, 19, 42, 665, DateTimeKind.Local).AddTicks(7509));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 10, 15, 19, 42, 665, DateTimeKind.Local).AddTicks(7511));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 10, 15, 19, 42, 665, DateTimeKind.Local).AddTicks(7512));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentNumbers");

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 8, 1, 57, 23, 884, DateTimeKind.Local).AddTicks(9877));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 8, 1, 57, 23, 884, DateTimeKind.Local).AddTicks(9921));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 8, 1, 57, 23, 884, DateTimeKind.Local).AddTicks(9923));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 8, 1, 57, 23, 884, DateTimeKind.Local).AddTicks(9925));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 8, 1, 57, 23, 884, DateTimeKind.Local).AddTicks(9927));
        }
    }
}
