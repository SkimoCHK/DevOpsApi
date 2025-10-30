using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApartadoAulasAPI.Migrations
{
    /// <inheritdoc />
    public partial class HistorialAccionesTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorialAcciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Accion = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    FechaAccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SolicitudId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAcciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialAcciones_SolicitudApartado_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "SolicitudApartado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialAcciones_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAcciones_SolicitudId",
                table: "HistorialAcciones",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAcciones_UsuarioId",
                table: "HistorialAcciones",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialAcciones");
        }
    }
}
