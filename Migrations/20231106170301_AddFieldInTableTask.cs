﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldInTableTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "Task",
                type: "Boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "Task");
        }
    }
}