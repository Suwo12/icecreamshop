using Microsoft.EntityFrameworkCore.Migrations;

namespace icecreamshop.Data.Migrations
{
    public partial class DroppedTableProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBox",
                table: "OrderBox");

            migrationBuilder.DropIndex(
                name: "IX_OrderBox_ProductId",
                table: "OrderBox");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderBox");

            migrationBuilder.RenameTable(
                name: "OrderBox",
                newName: "OrderBoxes");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBox_UserId",
                table: "OrderBoxes",
                newName: "IX_OrderBoxes_UserId");

            migrationBuilder.AddColumn<int>(
                name: "FlavourId",
                table: "OrderBoxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBoxes",
                table: "OrderBoxes",
                column: "OrderBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBoxes_FlavourId",
                table: "OrderBoxes",
                column: "FlavourId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBoxes_Flavour_FlavourId",
                table: "OrderBoxes",
                column: "FlavourId",
                principalTable: "Flavour",
                principalColumn: "FlavourId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBoxes_AspNetUsers_UserId",
                table: "OrderBoxes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBoxes_Flavour_FlavourId",
                table: "OrderBoxes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderBoxes_AspNetUsers_UserId",
                table: "OrderBoxes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBoxes",
                table: "OrderBoxes");

            migrationBuilder.DropIndex(
                name: "IX_OrderBoxes_FlavourId",
                table: "OrderBoxes");

            migrationBuilder.DropColumn(
                name: "FlavourId",
                table: "OrderBoxes");

            migrationBuilder.RenameTable(
                name: "OrderBoxes",
                newName: "OrderBox");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBoxes_UserId",
                table: "OrderBox",
                newName: "IX_OrderBox_UserId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderBox",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBox",
                table: "OrderBox",
                column: "OrderBoxId");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    FlavorId = table.Column<int>(type: "int", nullable: false),
                    FlavourId = table.Column<int>(type: "int", nullable: true),
                    OrderBoxId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Flavour_FlavourId",
                        column: x => x.FlavourId,
                        principalTable: "Flavour",
                        principalColumn: "FlavourId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderBox_ProductId",
                table: "OrderBox",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_FlavourId",
                table: "Product",
                column: "FlavourId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBox_Product_ProductId",
                table: "OrderBox",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBox_AspNetUsers_UserId",
                table: "OrderBox",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
