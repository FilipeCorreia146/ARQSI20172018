using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Semente.Migrations
{
    public partial class Farmaco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmacoId",
                table: "Medicamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamento_Farmaco_FarmacoId",
                table: "Medicamento");

            migrationBuilder.DropTable(
                name: "Farmaco");

            migrationBuilder.DropIndex(
                name: "IX_Medicamento_FarmacoId",
                table: "Medicamento");

            migrationBuilder.DropColumn(
                name: "FarmacoId",
                table: "Medicamento");
        }
    }
}
