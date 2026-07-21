using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailInventory.Migrations;

public partial class AddStockQuantity : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "StockQuantity",
            table: "Products",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 1,
            column: "StockQuantity",
            value: 15);

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 2,
            column: "StockQuantity",
            value: 30);

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 3,
            column: "StockQuantity",
            value: 20);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "StockQuantity",
            table: "Products");
    }
}
