using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BootcampMvp.Migrations
{
    /// <inheritdoc />
    public partial class AddCoursesAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "Email", "Name" },
                values: new object[] { 3, 21, "charlie@example.com", "Charlie" });
        }
    }
}
