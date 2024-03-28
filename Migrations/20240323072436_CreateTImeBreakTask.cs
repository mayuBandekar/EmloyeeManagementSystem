using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmloyeeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class CreateTImeBreakTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimesheetsMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimesheetDate = table.Column<DateTime>(type: "date", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    ActualWorkingHours = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    EMSUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetsMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimesheetsMaster_AspNetUsers_EMSUserId",
                        column: x => x.EMSUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BreakMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreakStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreakEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimesheetID = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakMaster_TimesheetsMaster_TimesheetID",
                        column: x => x.TimesheetID,
                        principalTable: "TimesheetsMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "varchar(100)", nullable: false),
                    TimesheetID = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskMaster_TimesheetsMaster_TimesheetID",
                        column: x => x.TimesheetID,
                        principalTable: "TimesheetsMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreakMaster_TimesheetID",
                table: "BreakMaster",
                column: "TimesheetID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_TimesheetID",
                table: "TaskMaster",
                column: "TimesheetID");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetsMaster_EMSUserId",
                table: "TimesheetsMaster",
                column: "EMSUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakMaster");

            migrationBuilder.DropTable(
                name: "TaskMaster");

            migrationBuilder.DropTable(
                name: "TimesheetsMaster");
        }
    }
}
