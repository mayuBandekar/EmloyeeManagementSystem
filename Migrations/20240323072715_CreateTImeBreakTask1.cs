using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmloyeeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class CreateTImeBreakTask1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreakMaster_TimesheetsMaster_TimesheetID",
                table: "BreakMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskMaster_TimesheetsMaster_TimesheetID",
                table: "TaskMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_TimesheetsMaster_AspNetUsers_EMSUserId",
                table: "TimesheetsMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimesheetsMaster",
                table: "TimesheetsMaster");

            migrationBuilder.RenameTable(
                name: "TimesheetsMaster",
                newName: "TimesheetMaster");

            migrationBuilder.RenameIndex(
                name: "IX_TimesheetsMaster_EMSUserId",
                table: "TimesheetMaster",
                newName: "IX_TimesheetMaster_EMSUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimesheetMaster",
                table: "TimesheetMaster",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BreakMaster_TimesheetMaster_TimesheetID",
                table: "BreakMaster",
                column: "TimesheetID",
                principalTable: "TimesheetMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskMaster_TimesheetMaster_TimesheetID",
                table: "TaskMaster",
                column: "TimesheetID",
                principalTable: "TimesheetMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimesheetMaster_AspNetUsers_EMSUserId",
                table: "TimesheetMaster",
                column: "EMSUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreakMaster_TimesheetMaster_TimesheetID",
                table: "BreakMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskMaster_TimesheetMaster_TimesheetID",
                table: "TaskMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_TimesheetMaster_AspNetUsers_EMSUserId",
                table: "TimesheetMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimesheetMaster",
                table: "TimesheetMaster");

            migrationBuilder.RenameTable(
                name: "TimesheetMaster",
                newName: "TimesheetsMaster");

            migrationBuilder.RenameIndex(
                name: "IX_TimesheetMaster_EMSUserId",
                table: "TimesheetsMaster",
                newName: "IX_TimesheetsMaster_EMSUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimesheetsMaster",
                table: "TimesheetsMaster",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BreakMaster_TimesheetsMaster_TimesheetID",
                table: "BreakMaster",
                column: "TimesheetID",
                principalTable: "TimesheetsMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskMaster_TimesheetsMaster_TimesheetID",
                table: "TaskMaster",
                column: "TimesheetID",
                principalTable: "TimesheetsMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimesheetsMaster_AspNetUsers_EMSUserId",
                table: "TimesheetsMaster",
                column: "EMSUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
