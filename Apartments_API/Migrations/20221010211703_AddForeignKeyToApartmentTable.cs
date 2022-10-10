using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apartment_API.Migrations
{
    public partial class AddForeignKeyToApartmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentID",
                table: "ApartmentNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 10, 23, 17, 3, 685, DateTimeKind.Local).AddTicks(1789));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 10, 23, 17, 3, 685, DateTimeKind.Local).AddTicks(1821));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 10, 23, 17, 3, 685, DateTimeKind.Local).AddTicks(1824));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 10, 23, 17, 3, 685, DateTimeKind.Local).AddTicks(1826));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 10, 23, 17, 3, 685, DateTimeKind.Local).AddTicks(1828));

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentNumbers_ApartmentID",
                table: "ApartmentNumbers",
                column: "ApartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentNumbers_Apartments_ApartmentID",
                table: "ApartmentNumbers",
                column: "ApartmentID",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentNumbers_Apartments_ApartmentID",
                table: "ApartmentNumbers");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentNumbers_ApartmentID",
                table: "ApartmentNumbers");

            migrationBuilder.DropColumn(
                name: "ApartmentID",
                table: "ApartmentNumbers");

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
    }
}
