using Microsoft.EntityFrameworkCore.Migrations;

namespace StudLab.Migrations
{
    public partial class UpdTableTransport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AVector",
                table: "Tables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BVector",
                table: "Tables",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AVector",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "BVector",
                table: "Tables");
        }
    }
}
