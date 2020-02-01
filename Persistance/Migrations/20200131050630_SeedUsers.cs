using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistance.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserTables",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { 1, "josh@life.com", "Josh" });

            migrationBuilder.InsertData(
                table: "UserTables",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { 2, "josasdfasdfh@life.com", "Joshua" });

            migrationBuilder.InsertData(
                table: "UserTables",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { 3, "joasdfsh@life.com", "Joshie" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserTables",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserTables",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserTables",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
