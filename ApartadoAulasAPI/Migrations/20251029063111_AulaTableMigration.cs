using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApartadoAulasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AulaTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    CapacidadEstudiantes = table.Column<int>(type: "integer", nullable: false),
                    Estatus = table.Column<bool>(type: "boolean", nullable: false),
                    TipoAulaId = table.Column<int>(type: "integer", nullable: false),
                    EdificioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aula_Edificio_EdificioId",
                        column: x => x.EdificioId,
                        principalTable: "Edificio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aula_TipoAula_TipoAulaId",
                        column: x => x.TipoAulaId,
                        principalTable: "TipoAula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aula_EdificioId",
                table: "Aula",
                column: "EdificioId");

            migrationBuilder.CreateIndex(
                name: "IX_Aula_TipoAulaId",
                table: "Aula",
                column: "TipoAulaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aula");
        }
    }
}
