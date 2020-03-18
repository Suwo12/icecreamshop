using Microsoft.EntityFrameworkCore.Migrations;

namespace icecreamshop.Data.Migrations
{
    public partial class AddingPriceToFlavoru : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FlavourPrice",
                table: "Flavour",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlavourPrice",
                table: "Flavour");
        }
    }
}
