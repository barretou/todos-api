using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class RenamingUserIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_Users_UserId",
                table: "tasks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tasks",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_UserId",
                table: "tasks",
                newName: "IX_tasks_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_Users_user_id",
                table: "tasks",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_Users_user_id",
                table: "tasks");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "tasks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_user_id",
                table: "tasks",
                newName: "IX_tasks_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_Users_UserId",
                table: "tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
