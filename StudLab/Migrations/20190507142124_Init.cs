using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StudLab.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmailUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatrixOperationTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Table = table.Column<string>(nullable: true),
                    NumRow = table.Column<int>(nullable: false),
                    NumColumn = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    TableTwo = table.Column<string>(nullable: true),
                    NumRowTwo = table.Column<int>(nullable: false),
                    NumColumnTwo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrixOperationTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatrixOperationTask_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MultiCriteriaTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Table = table.Column<string>(nullable: true),
                    NumRow = table.Column<int>(nullable: false),
                    NumColumn = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiCriteriaTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiCriteriaTables_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Table = table.Column<string>(nullable: true),
                    NumRow = table.Column<int>(nullable: false),
                    NumColumn = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    AVector = table.Column<string>(nullable: true),
                    BVector = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportTables_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatrixOperationTask_UserId",
                table: "MatrixOperationTask",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiCriteriaTables_UserId",
                table: "MultiCriteriaTables",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportTables_UserId",
                table: "TransportTables",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatrixOperationTask");

            migrationBuilder.DropTable(
                name: "MultiCriteriaTables");

            migrationBuilder.DropTable(
                name: "TransportTables");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
