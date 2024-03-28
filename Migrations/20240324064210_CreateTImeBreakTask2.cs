using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmloyeeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class CreateTImeBreakTask2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeID",
                table: "TimesheetMaster",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "TimesheetMaster",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");
        }
    }
}
