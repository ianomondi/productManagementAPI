using Microsoft.EntityFrameworkCore.Migrations;

namespace Supermarket.API.Migrations
{
    public partial class productvariantvariantid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_VariantOptions_VariantOptionId",
                table: "ProductVariants");

            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "ProductVariants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_VariantId",
                table: "ProductVariants",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Variants_VariantId",
                table: "ProductVariants",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_VariantOptions_VariantOptionId",
                table: "ProductVariants",
                column: "VariantOptionId",
                principalTable: "VariantOptions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Variants_VariantId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_VariantOptions_VariantOptionId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_VariantId",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "ProductVariants");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_VariantOptions_VariantOptionId",
                table: "ProductVariants",
                column: "VariantOptionId",
                principalTable: "VariantOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
