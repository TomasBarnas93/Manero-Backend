using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manero_Backend.Migrations
{
    /// <inheritdoc />
    public partial class FixedmodelStarRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "money",
                table: "Products",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "money");
        }
    }
}
