using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CobrArWeb.Migrations
{
    /// <inheritdoc />
    public partial class ModeMDP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModeDPId",
                table: "Ventes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ventes_ModeDPId",
                table: "Ventes",
                column: "ModeDPId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventes_MDPs_ModeDPId",
                table: "Ventes",
                column: "ModeDPId",
                principalTable: "MDPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventes_MDPs_ModeDPId",
                table: "Ventes");

            migrationBuilder.DropIndex(
                name: "IX_Ventes_ModeDPId",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "ModeDPId",
                table: "Ventes");
        }
    }
}
