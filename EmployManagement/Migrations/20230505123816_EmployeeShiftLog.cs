using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployManagement.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeShiftLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeShiftLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeShiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeShiftLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeShiftLog_EmployeeShift_EmployeeShiftId",
                        column: x => x.EmployeeShiftId,
                        principalTable: "EmployeeShift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeShiftLog_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeShiftLog_EmployeeId",
                table: "EmployeeShiftLog",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeShiftLog_EmployeeShiftId",
                table: "EmployeeShiftLog",
                column: "EmployeeShiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeShiftLog");
        }
    }
}
