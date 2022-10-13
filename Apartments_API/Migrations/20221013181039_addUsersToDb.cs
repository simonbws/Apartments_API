using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apartment_API.Migrations
{
    public partial class addUsersToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImagePath" },
                values: new object[] { new DateTime(2022, 10, 13, 20, 10, 39, 71, DateTimeKind.Local).AddTicks(2529), "https://dotnetmastery.com/bluevillaimages/villa3.jpg" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImagePath" },
                values: new object[] { new DateTime(2022, 10, 13, 20, 10, 39, 71, DateTimeKind.Local).AddTicks(2570), "https://dotnetmastery.com/bluevillaimages/villa1.jpg" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImagePath" },
                values: new object[] { new DateTime(2022, 10, 13, 20, 10, 39, 71, DateTimeKind.Local).AddTicks(2572), "https://dotnetmastery.com/bluevillaimages/villa4.jpg" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImagePath", "Name" },
                values: new object[] { new DateTime(2022, 10, 13, 20, 10, 39, 71, DateTimeKind.Local).AddTicks(2574), "https://dotnetmastery.com/bluevillaimages/villa5.jpg", "Diamond Apartment" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImagePath" },
                values: new object[] { new DateTime(2022, 10, 13, 20, 10, 39, 71, DateTimeKind.Local).AddTicks(2577), "https://dotnetmastery.com/bluevillaimages/villa2.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImagePath" },
                values: new object[] { new DateTime(2022, 10, 11, 22, 4, 56, 839, DateTimeKind.Local).AddTicks(7139), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImagePath" },
                values: new object[] { new DateTime(2022, 10, 11, 22, 4, 56, 839, DateTimeKind.Local).AddTicks(7180), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImagePath" },
                values: new object[] { new DateTime(2022, 10, 11, 22, 4, 56, 839, DateTimeKind.Local).AddTicks(7182), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImagePath", "Name" },
                values: new object[] { new DateTime(2022, 10, 11, 22, 4, 56, 839, DateTimeKind.Local).AddTicks(7184), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg", "Diamond Villa" });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImagePath" },
                values: new object[] { new DateTime(2022, 10, 11, 22, 4, 56, 839, DateTimeKind.Local).AddTicks(7186), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg" });
        }
    }
}
