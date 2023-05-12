using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manero_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColorEntity_Colors_ColorId",
                table: "ProductColorEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColorEntity_Products_ProductId",
                table: "ProductColorEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeEntity_Products_ProductId",
                table: "ProductSizeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeEntity_Sizes_SizeId",
                table: "ProductSizeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSizeEntity",
                table: "ProductSizeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColorEntity",
                table: "ProductColorEntity");

            migrationBuilder.RenameTable(
                name: "ProductSizeEntity",
                newName: "ProductSizes");

            migrationBuilder.RenameTable(
                name: "ProductColorEntity",
                newName: "ProductColors");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizeEntity_SizeId",
                table: "ProductSizes",
                newName: "IX_ProductSizes_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizeEntity_ProductId",
                table: "ProductSizes",
                newName: "IX_ProductSizes_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColorEntity_ProductId",
                table: "ProductColors",
                newName: "IX_ProductColors_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColorEntity_ColorId",
                table: "ProductColors",
                newName: "IX_ProductColors_ColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSizes",
                table: "ProductSizes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Colors_ColorId",
                table: "ProductColors",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Products_ProductId",
                table: "ProductColors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Products_ProductId",
                table: "ProductSizes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Sizes_SizeId",
                table: "ProductSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Colors_ColorId",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Products_ProductId",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Products_ProductId",
                table: "ProductSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Sizes_SizeId",
                table: "ProductSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSizes",
                table: "ProductSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.RenameTable(
                name: "ProductSizes",
                newName: "ProductSizeEntity");

            migrationBuilder.RenameTable(
                name: "ProductColors",
                newName: "ProductColorEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizes_SizeId",
                table: "ProductSizeEntity",
                newName: "IX_ProductSizeEntity_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizes_ProductId",
                table: "ProductSizeEntity",
                newName: "IX_ProductSizeEntity_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColors_ProductId",
                table: "ProductColorEntity",
                newName: "IX_ProductColorEntity_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColors_ColorId",
                table: "ProductColorEntity",
                newName: "IX_ProductColorEntity_ColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSizeEntity",
                table: "ProductSizeEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColorEntity",
                table: "ProductColorEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColorEntity_Colors_ColorId",
                table: "ProductColorEntity",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColorEntity_Products_ProductId",
                table: "ProductColorEntity",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeEntity_Products_ProductId",
                table: "ProductSizeEntity",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeEntity_Sizes_SizeId",
                table: "ProductSizeEntity",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");
        }
    }
}
