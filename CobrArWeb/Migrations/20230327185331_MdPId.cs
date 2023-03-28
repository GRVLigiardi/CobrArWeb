using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CobrArWeb.Migrations
{
    /// <inheritdoc />
    public partial class MdPId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModeDePaiementId",
                table: "Ventes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModeDePaiementId",
                table: "Ventes");
        }
    }
}
