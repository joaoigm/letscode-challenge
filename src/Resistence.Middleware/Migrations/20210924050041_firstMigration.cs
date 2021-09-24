﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Resistence.Middleware.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rebeldes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(maxLength: 80, nullable: false),
                    Idade = table.Column<int>(nullable: false),
                    Genero = table.Column<char>(nullable: false),
                    IndicacaoTraidor = table.Column<int>(nullable: false),
                    Traidor = table.Column<bool>(nullable: false),
                    JsonInventory = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rebeldes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localizacoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    RebeldeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localizacoes_Rebeldes_RebeldeId",
                        column: x => x.RebeldeId,
                        principalTable: "Rebeldes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rebeldes",
                columns: new[] { "Id", "Genero", "Idade", "IndicacaoTraidor", "JsonInventory", "Nome", "Traidor" },
                values: new object[] { 1, 'M', 17, 0, "{\"AGUA\":10,\"ARMA\":1,\"COMIDA\":20,\"MUNICAO\":50}", "Poe Dameron", false });

            migrationBuilder.InsertData(
                table: "Rebeldes",
                columns: new[] { "Id", "Genero", "Idade", "IndicacaoTraidor", "JsonInventory", "Nome", "Traidor" },
                values: new object[] { 2, 'M', 50, 0, "{\"AGUA\":10,\"ARMA\":1,\"COMIDA\":20,\"MUNICAO\":50}", "Leia Organa", false });

            migrationBuilder.InsertData(
                table: "Rebeldes",
                columns: new[] { "Id", "Genero", "Idade", "IndicacaoTraidor", "JsonInventory", "Nome", "Traidor" },
                values: new object[] { 3, 'O', 170, 0, "{\"AGUA\":10,\"ARMA\":1,\"COMIDA\":20,\"MUNICAO\":50}", "C3PO", false });

            migrationBuilder.InsertData(
                table: "Localizacoes",
                columns: new[] { "Id", "Latitude", "Longitude", "Nome", "RebeldeId" },
                values: new object[] { 1, "20 graus sul", "44 graus oeste", "Endor moon", 1 });

            migrationBuilder.InsertData(
                table: "Localizacoes",
                columns: new[] { "Id", "Latitude", "Longitude", "Nome", "RebeldeId" },
                values: new object[] { 2, "20 graus sul", "44 graus oeste", "Kamino", 2 });

            migrationBuilder.InsertData(
                table: "Localizacoes",
                columns: new[] { "Id", "Latitude", "Longitude", "Nome", "RebeldeId" },
                values: new object[] { 3, "20 graus sul", "44 graus oeste", "Naboo", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Localizacoes_RebeldeId",
                table: "Localizacoes",
                column: "RebeldeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localizacoes");

            migrationBuilder.DropTable(
                name: "Rebeldes");
        }
    }
}
