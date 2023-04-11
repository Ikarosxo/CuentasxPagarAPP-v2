using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CuentasxPagarAPP_v2.Data.Migrations
{
    public partial class _1stMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conceptos",
                columns: table => new
                {
                    IdConcepto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conceptos", x => x.IdConcepto);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    IdProveedor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    TipoPersona = table.Column<string>(type: "text", nullable: false),
                    CedulaRNC = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.IdProveedor);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosxPagar",
                columns: table => new
                {
                    NumDocument = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumFacturaPagar = table.Column<int>(type: "integer", nullable: false),
                    FechaDocumento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Monto = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdProveedor = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosxPagar", x => x.NumDocument);
                    table.ForeignKey(
                        name: "FK_DocumentosxPagar_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosxPagar_IdProveedor",
                table: "DocumentosxPagar",
                column: "IdProveedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conceptos");

            migrationBuilder.DropTable(
                name: "DocumentosxPagar");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
