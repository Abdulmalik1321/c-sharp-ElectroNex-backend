using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class custombridge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_wishlist_product_products_id",
                table: "product_wishlist");

            migrationBuilder.DropForeignKey(
                name: "fk_product_wishlist_wishlist_wishlists_id",
                table: "product_wishlist");

            migrationBuilder.RenameColumn(
                name: "wishlists_id",
                table: "product_wishlist",
                newName: "wishlist_id");

            migrationBuilder.RenameColumn(
                name: "products_id",
                table: "product_wishlist",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "ix_product_wishlist_wishlists_id",
                table: "product_wishlist",
                newName: "ix_product_wishlist_wishlist_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_wishlist_product_product_id",
                table: "product_wishlist",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_wishlist_wishlist_wishlist_id",
                table: "product_wishlist",
                column: "wishlist_id",
                principalTable: "wishlist",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_wishlist_product_product_id",
                table: "product_wishlist");

            migrationBuilder.DropForeignKey(
                name: "fk_product_wishlist_wishlist_wishlist_id",
                table: "product_wishlist");

            migrationBuilder.RenameColumn(
                name: "wishlist_id",
                table: "product_wishlist",
                newName: "wishlists_id");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "product_wishlist",
                newName: "products_id");

            migrationBuilder.RenameIndex(
                name: "ix_product_wishlist_wishlist_id",
                table: "product_wishlist",
                newName: "ix_product_wishlist_wishlists_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_wishlist_product_products_id",
                table: "product_wishlist",
                column: "products_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_wishlist_wishlist_wishlists_id",
                table: "product_wishlist",
                column: "wishlists_id",
                principalTable: "wishlist",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
