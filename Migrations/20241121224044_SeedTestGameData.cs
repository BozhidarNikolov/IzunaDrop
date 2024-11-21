using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IzunaDrop.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestGameData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Developer", "Genre", "ImagePath", "Name", "Publisher", "ReleaseDate" },
                values: new object[] { 1, "An action-packed adventure game.", "", 0, null, "TestGame:The testing", "", new DateTime(2006, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
