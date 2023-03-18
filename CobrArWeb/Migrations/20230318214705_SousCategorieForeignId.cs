using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CobrArWeb.Migrations
{
    /// <inheritdoc />
    public partial class SousCategorieForeignId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategorieId",
                table: "SousCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SousCategories_CategorieId",
                table: "SousCategories",
                column: "CategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_SousCategories_Categories_CategorieId",
                table: "SousCategories",
                column: "CategorieId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SousCategories_Categories_CategorieId",
                table: "SousCategories");

            migrationBuilder.DropIndex(
                name: "IX_SousCategories_CategorieId",
                table: "SousCategories");

            migrationBuilder.DropColumn(
                name: "CategorieId",
                table: "SousCategories");
        }
    }
}
