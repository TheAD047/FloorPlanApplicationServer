using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FloorPlanApplication.Migrations
{
    /// <inheritdoc />
    public partial class refactor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UaerRole",
                table: "AspNetUsers",
                newName: "UserRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRole",
                table: "AspNetUsers",
                newName: "UaerRole");
        }
    }
}
