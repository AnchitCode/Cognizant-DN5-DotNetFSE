using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailInventory.Migrations;

public partial class AddProductDetailTagAndRowVersion : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<byte[]>(
            name: "RowVersion",
            table: "Products",
            type: "rowversion",
            rowVersion: true,
            nullable: false,
            defaultValue: new byte[0]);

        migrationBuilder.CreateTable(
            name: "Tags",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tags", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ProductDetails",
            columns: table => new
            {
                ProductDetailId = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                WarrantyInfo = table.Column<string>(maxLength: 200, nullable: false),
                ProductId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductDetails", x => x.ProductDetailId);
                table.ForeignKey(
                    name: "FK_ProductDetails_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ProductTag",
            columns: table => new
            {
                ProductId = table.Column<int>(nullable: false),
                TagId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductTag", x => new { x.ProductId, x.TagId });
                table.ForeignKey(
                    name: "FK_ProductTag_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ProductTag_Tags_TagId",
                    column: x => x.TagId,
                    principalTable: "Tags",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Tags_Name",
            table: "Tags",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ProductDetails_ProductId",
            table: "ProductDetails",
            column: "ProductId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ProductTag_TagId",
            table: "ProductTag",
            column: "TagId");

        migrationBuilder.InsertData(
            table: "Tags",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Featured" },
                { 2, "Popular" },
                { 3, "Clearance" }
            });

        migrationBuilder.InsertData(
            table: "ProductDetails",
            columns: new[] { "ProductDetailId", "WarrantyInfo", "ProductId" },
            values: new object[,]
            {
                { 1, "2 years manufacturer warranty", 1 },
                { 2, "1 year manufacturer warranty", 2 },
                { 3, "18 months service warranty", 3 }
            });

        migrationBuilder.InsertData(
            table: "ProductTag",
            columns: new[] { "ProductId", "TagId" },
            values: new object[,]
            {
                { 1, 1 },
                { 1, 2 },
                { 2, 2 },
                { 3, 3 }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ProductTag");

        migrationBuilder.DropTable(
            name: "ProductDetails");

        migrationBuilder.DropTable(
            name: "Tags");

        migrationBuilder.DropColumn(
            name: "RowVersion",
            table: "Products");
    }
}
