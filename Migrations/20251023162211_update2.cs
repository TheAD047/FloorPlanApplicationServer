using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FloorPlanApplication.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_AspNetUsers_ClietnID",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesLogs_Services_ServiceID",
                table: "ServicesLogs");

            migrationBuilder.RenameColumn(
                name: "ClietnID",
                table: "OrderItems",
                newName: "ClientID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ClietnID",
                table: "OrderItems",
                newName: "IX_OrderItems_ClientID");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceID",
                table: "ServicesLogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_AspNetUsers_ClientID",
                table: "OrderItems",
                column: "ClientID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesLogs_Services_ServiceID",
                table: "ServicesLogs",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_AspNetUsers_ClientID",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesLogs_Services_ServiceID",
                table: "ServicesLogs");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "OrderItems",
                newName: "ClietnID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ClientID",
                table: "OrderItems",
                newName: "IX_OrderItems_ClietnID");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceID",
                table: "ServicesLogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_AspNetUsers_ClietnID",
                table: "OrderItems",
                column: "ClietnID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesLogs_Services_ServiceID",
                table: "ServicesLogs",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "ID");
        }
    }
}
