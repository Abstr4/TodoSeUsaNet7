using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoSeUsaNet7.Migrations
{
    /// <inheritdoc />
    public partial class rename_clienteId_to_clientId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Product",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Product",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Product",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Condicion",
                table: "Product",
                newName: "Condition");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Client",
                newName: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Product",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Product",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "Product",
                newName: "Condicion");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Client",
                newName: "ClienteId");
        }
    }
}
