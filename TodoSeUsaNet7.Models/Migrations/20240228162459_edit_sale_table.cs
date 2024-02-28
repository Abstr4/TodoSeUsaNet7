using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoSeUsaNet7.Migrations
{
    /// <inheritdoc />
    public partial class edit_sale_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Sale_SaleId",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_SaleId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Bill");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "Bill",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bill_SaleId",
                table: "Bill",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Sale_SaleId",
                table: "Bill",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "SaleId");
        }
    }
}
