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
                    Id = table.Column<long>(nullable: false)
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
                    GameId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
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
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { 40220438528782809L, "Goblin Battle", 1L });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { 50220438528782809L, "Orc Battle", 1L });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { 60220438528782809L, "Dragon Battle", 1L });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { 70220438528782809L, "Minotaur Battle", 2L });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { 10220438528782809L, "joshisplutar@gmail.com", "Josh Wren" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { 20220438528782809L, "josasdfasdfh@life.com", "Tim" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { 30220438528782809L, "joasdfsh@life.com", "Bob" });

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
