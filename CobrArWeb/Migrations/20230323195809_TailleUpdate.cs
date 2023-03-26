using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CobrArWeb.Migrations
{
    /// <inheritdoc />
    public partial class TailleUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taille",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "TailleId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_TailleId",
                table: "Products",
                column: "TailleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Tailles_TailleId",
                table: "Products",
                column: "TailleId",
                principalTable: "Tailles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Tailles_TailleId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_TailleId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TailleId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Taille",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
