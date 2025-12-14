using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FloorPlanApplication.Migrations
{
    /// <inheritdoc />
    public partial class update10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ServiceAcceptanceDate",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ServiceCompletionDate",
                table: "Services",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceAcceptanceDate",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceCompletionDate",
                table: "Services");
        }
    }
}
