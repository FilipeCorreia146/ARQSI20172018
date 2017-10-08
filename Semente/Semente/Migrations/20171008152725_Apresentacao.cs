using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Semente.Migrations
{
    public partial class Apresentacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamento_Farmaco_FarmacoId",
                table: "Medicamento");

            migrationBuilder.DropIndex(
                name: "IX_Medicamento_FarmacoId",
                table: "Medicamento");

            migrationBuilder.DropColumn(
                name: "FarmacoId",
                table: "Medicamento");

            migrationBuilder.AddColumn<int>(
                name: "ApresentacaoId",
                table: "Medicamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ApresentacaoId1",
                table: "Medicamento",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Apresentacao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Concentracao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FarmacoId = table.Column<int>(type: "int", nullable: false),
                    Forma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qtd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apresentacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apresentacao_Farmaco_FarmacoId",
                        column: x => x.FarmacoId,
                        principalTable: "Farmaco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_ApresentacaoId1",
                table: "Medicamento",
                column: "ApresentacaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Apresentacao_FarmacoId",
                table: "Apresentacao",
                column: "FarmacoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamento_Apresentacao_ApresentacaoId1",
                table: "Medicamento",
                column: "ApresentacaoId1",
                principalTable: "Apresentacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamento_Apresentacao_ApresentacaoId1",
                table: "Medicamento");

            migrationBuilder.DropTable(
                name: "Apresentacao");

            migrationBuilder.DropIndex(
                name: "IX_Medicamento_ApresentacaoId1",
                table: "Medicamento");

            migrationBuilder.DropColumn(
                name: "ApresentacaoId",
                table: "Medicamento");

            migrationBuilder.DropColumn(
                name: "ApresentacaoId1",
                table: "Medicamento");

            migrationBuilder.AddColumn<int>(
                name: "FarmacoId",
                table: "Medicamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_FarmacoId",
                table: "Medicamento",
                column: "FarmacoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamento_Farmaco_FarmacoId",
                table: "Medicamento",
                column: "FarmacoId",
                principalTable: "Farmaco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
