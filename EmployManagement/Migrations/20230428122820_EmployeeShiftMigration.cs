using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployManagement.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeShiftMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_IdentityUserId",
                table: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeShift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDuration = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeShift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeShift_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeShift_EmployeeId",
                table: "EmployeeShift",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_IdentityUserId",
                table: "Employee",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_IdentityUserId",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "EmployeeShift");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_IdentityUserId",
                table: "Employee",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
