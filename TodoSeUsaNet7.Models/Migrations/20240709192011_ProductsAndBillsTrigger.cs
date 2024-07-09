using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoSeUsaNet7.Models.Migrations
{
    /// <inheritdoc />
    public partial class ProductsAndBillsTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"
            CREATE TRIGGER UpdateBillOnProductChange
            ON Product
            AFTER INSERT, UPDATE
            AS
            BEGIN
                    DECLARE @BillId INT = (SELECT BillId FROM inserted);
                    UPDATE Bill 
                    SET TotalProducts = (SELECT COUNT(ProductId) FROM Product WHERE BillId = @BillId)
                    WHERE BillId = @BillId;

                    UPDATE Bill 
                    SET ProductsSold = (SELECT COUNT(ProductId) FROM Product WHERE BillId = @BillId AND Sold = 1)
                    WHERE BillId = @BillId;

                    UPDATE Bill 
                    SET TotalAmountPerProducts = (SELECT ISNULL(SUM(Price), 0) FROM Product WHERE BillId = @BillId)
                    WHERE BillId = @BillId;

                    UPDATE Bill 
                    SET TotalAmountSold = (SELECT ISNULL(SUM(Price), 0) FROM Product WHERE BillId = @BillId AND Sold = 1)
                    WHERE BillId = @BillId;
            END
            ");

            migrationBuilder.Sql(
            @"
            CREATE TRIGGER UpdateClientOnBillChange
            ON Bill
            AFTER INSERT, UPDATE
            AS
            BEGIN
                DECLARE @ClientId int = (SELECT ClientId FROM inserted)

                UPDATE Client SET TotalProducts = (SELECT ISNULL(sum(TotalProducts), 0) FROM Bill WHERE ClientId = @ClientId)
                WHERE ClientId = @ClientId;

                UPDATE Client SET TotalAmountPerProducts = (SELECT ISNULL(sum(TotalAmountPerProducts), 0)git  FROM Bill WHERE ClientId = @ClientId)
                WHERE ClientId = @ClientId;

                UPDATE Client SET ProductsSold = (SELECT ISNULL(sum(ProductsSold), 0) FROM Bill WHERE ClientId = @ClientId)
                WHERE ClientId = @ClientId;

                UPDATE Client SET TotalAmountSold = (SELECT ISNULL(sum(TotalAmountSold), 0) FROM Bill WHERE ClientId = @ClientId)
                WHERE ClientId = @ClientId;

                    UPDATE Client SET TotalBills = (SELECT count(BillId) FROM Bill WHERE ClientId = @ClientId)
                WHERE ClientId = @ClientId;
            END
            ");
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Bill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 20, 11, 117, DateTimeKind.Local).AddTicks(3656),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 19, 14, 462, DateTimeKind.Local).AddTicks(1517));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Bill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 19, 14, 462, DateTimeKind.Local).AddTicks(1517),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 20, 11, 117, DateTimeKind.Local).AddTicks(3656));
        }
    }
}
