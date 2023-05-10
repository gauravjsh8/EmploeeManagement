using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployManagement.Migrations
{
    /// <inheritdoc />
    public partial class Employeesh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeDepartment",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDepartment");
        }
    }
}
