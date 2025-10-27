using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FloorPlanApplication.Migrations
{
    /// <inheritdoc />
    public partial class refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "AspNetUsers",
                newName: "EmployeeNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeNumber",
                table: "AspNetUsers",
                newName: "EmployeeID");
        }
    }
}
