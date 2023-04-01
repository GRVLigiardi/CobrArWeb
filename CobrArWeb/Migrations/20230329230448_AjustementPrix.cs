using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CobrArWeb.Migrations
{
    /// <inheritdoc />
    public partial class AjustementPrix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AjustementPrix",
                table: "Ventes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AjustementPrix",
                table: "Ventes");
        }
    }
}
