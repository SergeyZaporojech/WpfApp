using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibData.Migrations
{
    public partial class addPriseintblProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Prise",
                table: "tblProducts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prise",
                table: "tblProducts");
        }
    }
}
