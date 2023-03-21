using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CobrArWeb.Migrations
{
    /// <inheritdoc />
    public partial class VentesEquipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Equipe",
                table: "Ventes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipe",
                table: "Ventes");
        }
    }
}
