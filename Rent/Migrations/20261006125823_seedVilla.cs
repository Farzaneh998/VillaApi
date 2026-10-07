using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rent.Migrations
{
    /// <inheritdoc />
    public partial class seedVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreateDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft" },
                values: new object[,]
                {
                    { 1, "tets", new DateTime(2026, 10, 6, 16, 28, 23, 431, DateTimeKind.Local).AddTicks(1631), "best villa", "https://www.golbazar.com/%D8%B3%D9%81%D8%A7%D8%B1%D8%B4-%DA%AF%D9%84-%D9%88-%D9%87%D8%AF%DB%8C%D9%87-%D8%A8%D9%87-%D8%B3%D8%B1%D8%A7%D8%B3%D8%B1-%D8%A7%DB%8C%D8%B1%D8%A7%D9%86", "villa1", 20, 100.0, 10 },
                    { 2, "tets2", new DateTime(2026, 10, 6, 16, 28, 23, 431, DateTimeKind.Local).AddTicks(1645), "best villa", "https://www.golbazar.com/%D8%B3%D9%81%D8%A7%D8%B1%D8%B4-%DA%AF%D9%84-%D9%88-%D9%87%D8%AF%DB%8C%D9%87-%D8%A8%D9%87-%D8%B3%D8%B1%D8%A7%D8%B3%D8%B1-%D8%A7%DB%8C%D8%B1%D8%A7%D9%86", "villa2", 50, 1000.0, 20 },
                    { 3, "hjf", new DateTime(2026, 10, 6, 16, 28, 23, 431, DateTimeKind.Local).AddTicks(1647), "best villa5", "https://www.golbazar.com/%D8%B3%D9%81%D8%A7%D8%B1%D8%B4-%DA%AF%D9%84-%D9%88-%D9%87%D8%AF%DB%8C%D9%87-%D8%A8%D9%87-%D8%B3%D8%B1%D8%A7%D8%B3%D8%B1-%D8%A7%DB%8C%D8%B1%D8%A7%D9%86", "villa3", 505, 1000.0, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
