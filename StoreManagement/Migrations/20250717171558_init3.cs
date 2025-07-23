using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIStoreManagement.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sales",
                newName: "CustomerName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Sales",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Sales",
                type: "int",
                nullable: true);
        }
    }
}
