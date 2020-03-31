using System;
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
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
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
                table: "Users",
                columns: new[] { "Id", "UserName" },
                values: new object[] { 10220438528782809L, "Josh Wren" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "UserName" },
                values: new object[] { 20220438528782809L, "Tim" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "UserName" },
                values: new object[] { 30220438528782809L, "Bob" });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { new Guid("899f04dd-064d-477a-a45d-e6b9a03aa970"), "Goblin Battle", 10220438528782809L });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { new Guid("5eb46df3-a99d-4ecb-b88d-71becf1bc026"), "Orc Battle", 10220438528782809L });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { new Guid("a5310bf9-b45c-41b7-b732-2122245122e6"), "Dragon Battle", 20220438528782809L });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName", "UserId" },
                values: new object[] { new Guid("2fc4493f-bc85-40c1-a82a-7fcd02d03b29"), "Minotaur Battle", 30220438528782809L });

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
