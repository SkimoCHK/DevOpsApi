using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApartadoAulasAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsuarioTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Usuario",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Usuario");
        }
    }
}
