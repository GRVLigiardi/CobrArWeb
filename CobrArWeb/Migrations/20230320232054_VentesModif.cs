using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CobrArWeb.Migrations
{
    /// <inheritdoc />
    public partial class VentesModif : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categorie",
                table: "Ventes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodeBarre",
                table: "Ventes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fournisseur",
                table: "Ventes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Prix",
                table: "Ventes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Produit",
                table: "Ventes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantite",
                table: "Ventes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SousCategorie",
                table: "Ventes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Taille",
                table: "Ventes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categorie",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "CodeBarre",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "Fournisseur",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "Prix",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "Produit",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "Quantite",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "SousCategorie",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "Taille",
                table: "Ventes");
        }
    }
}
