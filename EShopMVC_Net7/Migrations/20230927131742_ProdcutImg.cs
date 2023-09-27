using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopMVC_Net7.Migrations
{
    /// <inheritdoc />
    public partial class ProdcutImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "AppProductImages",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductImages_ProductId",
                table: "AppProductImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductImages_AppProducts_ProductId",
                table: "AppProductImages",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductImages_AppProducts_ProductId",
                table: "AppProductImages");

            migrationBuilder.DropIndex(
                name: "IX_AppProductImages_ProductId",
                table: "AppProductImages");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "AppProductImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);
        }
    }
}
