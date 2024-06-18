using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoSeUsaNet7.Models.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientOnBillChangeTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"
            CREATE TRIGGER UpdateClientOnBillChange
            ON Bill
            AFTER INSERT, UPDATE
            AS
            BEGIN
	            DECLARE @ClientId int = (SELECT ClientId FROM inserted)

	            UPDATE Client SET TotalProducts = (SELECT sum(TotalProducts) FROM Bill WHERE ClientId = @ClientId)
	            WHERE ClientId = @ClientId;

	            UPDATE Client SET TotalAmountPerProducts = (SELECT sum(TotalAmountPerProducts) FROM Bill WHERE ClientId = @ClientId)
	            WHERE ClientId = @ClientId;

	            UPDATE Client SET ProductsSold = (SELECT sum(ProductsSold) FROM Bill WHERE ClientId = @ClientId)
	            WHERE ClientId = @ClientId;

	            UPDATE Client SET TotalAmountSold = (SELECT sum(TotalAmountSold) FROM Bill WHERE ClientId = @ClientId)
	            WHERE ClientId = @ClientId;

		            UPDATE Client SET TotalBills = (SELECT count(BillId) FROM Bill WHERE ClientId = @ClientId)
	            WHERE ClientId = @ClientId;
            END
            ");
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfIssue",
                table: "Sale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 18, 1, 36, 4, 980, DateTimeKind.Local).AddTicks(4146),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 18, 1, 24, 59, 913, DateTimeKind.Local).AddTicks(8412));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Bill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 18, 1, 36, 4, 980, DateTimeKind.Local).AddTicks(837),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 18, 1, 24, 59, 913, DateTimeKind.Local).AddTicks(2155));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"
            DROP TRIGGER IF EXISTS UpdateClientOnBillChange;
            ");
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfIssue",
                table: "Sale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 18, 1, 24, 59, 913, DateTimeKind.Local).AddTicks(8412),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 18, 1, 36, 4, 980, DateTimeKind.Local).AddTicks(4146));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Bill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 18, 1, 24, 59, 913, DateTimeKind.Local).AddTicks(2155),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 18, 1, 36, 4, 980, DateTimeKind.Local).AddTicks(837));
        }
    }
}
