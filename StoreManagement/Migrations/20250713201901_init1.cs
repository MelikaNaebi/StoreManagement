using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIStoreManagement.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothings_Sizes_SizesId",
                table: "Clothings");

            migrationBuilder.DropIndex(
                name: "IX_Clothings_SizesId",
                table: "Clothings");

            migrationBuilder.DropColumn(
                name: "SizesId",
                table: "Clothings");

            migrationBuilder.CreateIndex(
                name: "IX_Clothings_SizeId",
                table: "Clothings",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothings_Sizes_SizeId",
                table: "Clothings",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothings_Sizes_SizeId",
                table: "Clothings");

            migrationBuilder.DropIndex(
                name: "IX_Clothings_SizeId",
                table: "Clothings");

            migrationBuilder.AddColumn<int>(
                name: "SizesId",
                table: "Clothings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clothings_SizesId",
                table: "Clothings",
                column: "SizesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothings_Sizes_SizesId",
                table: "Clothings",
                column: "SizesId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
