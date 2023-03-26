using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CobrArWeb.Migrations
{
    /// <inheritdoc />
    public partial class LiaisonsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_CategorieId",
                table: "Products",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_EquipeId",
                table: "Products",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FournisseurId",
                table: "Products",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SousCategorieId",
                table: "Products",
                column: "SousCategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategorieId",
                table: "Products",
                column: "CategorieId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Equipes_EquipeId",
                table: "Products",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Fournisseurs_FournisseurId",
                table: "Products",
                column: "FournisseurId",
                principalTable: "Fournisseurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SousCategories_SousCategorieId",
                table: "Products",
                column: "SousCategorieId",
                principalTable: "SousCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategorieId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Equipes_EquipeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Fournisseurs_FournisseurId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SousCategories_SousCategorieId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategorieId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_EquipeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FournisseurId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SousCategorieId",
                table: "Products");
        }
    }
}
