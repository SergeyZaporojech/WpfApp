using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibData.Migrations
{
    public partial class AddcreatedcolumUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "tblUserrs",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "tblUserrs");
        }
    }
}
