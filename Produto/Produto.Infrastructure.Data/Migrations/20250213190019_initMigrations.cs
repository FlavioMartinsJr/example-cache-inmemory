using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Produto.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_produto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    valor = table.Column<decimal>(type: "numeric", nullable: true),
                    data_criado = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    data_alterado = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_produto_pkey", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "id_titulo_unico",
                table: "tbl_produto",
                columns: new[] { "titulo", "id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_produto");
        }
    }
}
