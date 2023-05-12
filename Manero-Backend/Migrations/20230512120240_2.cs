using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manero_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductColorEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColorEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductColorEntity_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductColorEntity_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSizeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSizeEntity_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductSizeEntity_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductColorEntity_ColorId",
                table: "ProductColorEntity",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColorEntity_ProductId",
                table: "ProductColorEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeEntity_ProductId",
                table: "ProductSizeEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeEntity_SizeId",
                table: "ProductSizeEntity",
                column: "SizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductColorEntity");

            migrationBuilder.DropTable(
                name: "ProductSizeEntity");
        }
    }
}
