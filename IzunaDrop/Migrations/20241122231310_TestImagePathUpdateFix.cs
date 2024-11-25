using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IzunaDrop.Migrations
{
    /// <inheritdoc />
    public partial class TestImagePathUpdateFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "/images/bigstock-test-icon-63758263-4108836978.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "C:\\Programming\\IzunaDrop\\IzunaDrop\\wwwroot\\bigstock-test-icon-63758263-4108836978.jpg");
        }
    }
}
