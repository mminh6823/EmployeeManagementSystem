using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Library.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CivilId",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "FileNumber",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "CivilId",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "FileNumber",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "CivilId",
                table: "Overtimes");

            migrationBuilder.DropColumn(
                name: "FileNumber",
                table: "Overtimes");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Overtimes");

            migrationBuilder.DropColumn(
                name: "CivilId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "FileNumber",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Vacations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Sanctions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Overtimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Overtimes");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "CivilId",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileNumber",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CivilId",
                table: "Sanctions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileNumber",
                table: "Sanctions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Sanctions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CivilId",
                table: "Overtimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileNumber",
                table: "Overtimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Overtimes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CivilId",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileNumber",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
