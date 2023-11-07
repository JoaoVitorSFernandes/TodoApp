using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "NVARCHAR", maxLength: 1023, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Status = table.Column<bool>(type: "Boolean", nullable: false, defaultValue: false),
                    Favorite = table.Column<bool>(type: "Boolean", nullable: false, defaultValue: false),
                    Date = table.Column<DateTime>(type: "SMALLDATETIME", maxLength: 60, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "SMALLDATETIME", maxLength: 60, nullable: false),
                    ListTasksId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Task",
                        column: x => x.ListTasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "NVARCHAR", maxLength: 1023, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Status = table.Column<bool>(type: "Boolean", nullable: false, defaultValue: false),
                    Favorite = table.Column<bool>(type: "Boolean", nullable: false, defaultValue: false),
                    Date = table.Column<DateTime>(type: "SMALLDATETIME", maxLength: 60, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "SMALLDATETIME", maxLength: 60, nullable: false),
                    TodoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTaskId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_SubTask",
                        column: x => x.TodoId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubTask_TodoId",
                table: "SubTask",
                column: "TodoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_ListTasksId",
                table: "Task",
                column: "ListTasksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubTask");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
