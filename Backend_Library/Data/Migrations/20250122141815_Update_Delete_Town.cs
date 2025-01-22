using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Library.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Delete_Town : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Towns_TownId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TownId",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towns_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TownId",
                table: "Employees",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_CityId",
                table: "Towns",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Towns_TownId",
                table: "Employees",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
