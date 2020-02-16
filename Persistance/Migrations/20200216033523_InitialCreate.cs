using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistance.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Game_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { 1, "joshisplutar@gmail.com", "Josh Wren" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { 2, "josasdfasdfh@life.com", "Tim" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { 3, "joasdfsh@life.com", "Bob" });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { 1, "Goblin Battle", 1 });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { 2, "Orc Battle", 1 });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { 3, "Dragon Battle", 1 });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { 4, "Minotaur Battle", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Game_UserId",
                table: "Game",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
