using Microsoft.EntityFrameworkCore.Migrations;

namespace icecreamshop.Data.Migrations
{
    public partial class ChangesToFlavourModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Flavour",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Flavour");
        }
    }
}
