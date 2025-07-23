using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIStoreManagement.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Orders",
                newName: "ClothingId");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "Orders",
                newName: "CustomerPhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClothingId",
                table: "Orders",
                column: "ClothingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Clothings_ClothingId",
                table: "Orders",
                column: "ClothingId",
                principalTable: "Clothings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Clothings_ClothingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ClothingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CustomerPhoneNumber",
                table: "Orders",
                newName: "Fullname");

            migrationBuilder.RenameColumn(
                name: "ClothingId",
                table: "Orders",
                newName: "Quantity");
        }
    }
}
