using Microsoft.EntityFrameworkCore.Migrations;

namespace StudLab.Migrations
{
    public partial class RenameTableInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Transports_TransportId",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "TransportId",
                table: "Tables",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_TransportId",
                table: "Tables",
                newName: "IX_Tables_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Transports_UserId",
                table: "Tables",
                column: "UserId",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Transports_UserId",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tables",
                newName: "TransportId");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_UserId",
                table: "Tables",
                newName: "IX_Tables_TransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Transports_TransportId",
                table: "Tables",
                column: "TransportId",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
