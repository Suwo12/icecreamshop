using Microsoft.EntityFrameworkCore.Migrations;

namespace icecreamshop.Data.Migrations
{
    public partial class ChangesToTableProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlavorId",
                table: "Product",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlavorId",
                table: "Product");
        }
    }
}
