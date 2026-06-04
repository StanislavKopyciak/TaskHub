using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "EmailVerifications");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedAt",
                table: "EmailVerifications",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "EmailVerifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EmailVerifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EmailVerifications");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "EmailVerifications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
