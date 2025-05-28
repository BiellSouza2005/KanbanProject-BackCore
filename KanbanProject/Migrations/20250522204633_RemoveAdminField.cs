using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanProject.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdminField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AdminId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AdminId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AdminId",
                table: "Tasks",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AdminId",
                table: "Tasks",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
