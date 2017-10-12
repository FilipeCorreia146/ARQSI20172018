using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Semente.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farmaco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmaco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apresentacao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Concentracao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FarmacoId = table.Column<int>(type: "int", nullable: false),
                    Forma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicamentoId = table.Column<int>(type: "int", nullable: false),
                    MedicamentoId1 = table.Column<long>(type: "bigint", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Apresentacao_Medicamento_MedicamentoId1",
                        column: x => x.MedicamentoId1,
                        principalTable: "Medicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posologia",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApresentacaoId = table.Column<int>(type: "int", nullable: false),
                    ApresentacaoId1 = table.Column<long>(type: "bigint", nullable: true),
                    Dose = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posologia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posologia_Apresentacao_ApresentacaoId1",
                        column: x => x.ApresentacaoId1,
                        principalTable: "Apresentacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apresentacao_FarmacoId",
                table: "Apresentacao",
                column: "FarmacoId");

            migrationBuilder.CreateIndex(
                name: "IX_Apresentacao_MedicamentoId1",
                table: "Apresentacao",
                column: "MedicamentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Posologia_ApresentacaoId1",
                table: "Posologia",
                column: "ApresentacaoId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posologia");

            migrationBuilder.DropTable(
                name: "Apresentacao");

            migrationBuilder.DropTable(
                name: "Farmaco");

            migrationBuilder.DropTable(
                name: "Medicamento");
        }
    }
}
