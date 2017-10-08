using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Semente.Migrations
{
    public partial class Posologia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_Posologia_ApresentacaoId1",
                table: "Posologia",
                column: "ApresentacaoId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posologia");
        }
    }
}
