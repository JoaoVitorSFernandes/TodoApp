using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class Fix_SubtaskTask_Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubTask_TodoId",
                table: "SubTask");

            migrationBuilder.DropColumn(
                name: "TasksId",
                table: "Task");

            migrationBuilder.CreateIndex(
                name: "IX_SubTask_TodoId",
                table: "SubTask",
                column: "TodoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubTask_TodoId",
                table: "SubTask");

            migrationBuilder.AddColumn<int>(
                name: "TasksId",
                table: "Task",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubTask_TodoId",
                table: "SubTask",
                column: "TodoId");
        }
    }
}
