using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CobrArWeb.Migrations
{
    /// <inheritdoc />
    public partial class CarteCadeau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MDPId2",
                table: "Ventes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MDPNom2",
                table: "Ventes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MDPId2",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "MDPNom2",
                table: "Ventes");
        }
    }
}
