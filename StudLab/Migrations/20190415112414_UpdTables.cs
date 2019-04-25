using Microsoft.EntityFrameworkCore.Migrations;

namespace StudLab.Migrations
{
    public partial class UpdTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Transports",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tables",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumColumn",
                table: "Tables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumRow",
                table: "Tables",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "NumColumn",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "NumRow",
                table: "Tables");
        }
    }
}
