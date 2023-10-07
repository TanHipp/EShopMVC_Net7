using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopMVC_Net7.Migrations
{
    /// <inheritdoc />
    public partial class CategoryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppCategorys_CategoryId",
                table: "AppProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppCategorys_CategoryId",
                table: "AppProducts",
                column: "CategoryId",
                principalTable: "AppCategorys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppCategorys_CategoryId",
                table: "AppProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppCategorys_CategoryId",
                table: "AppProducts",
                column: "CategoryId",
                principalTable: "AppCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
