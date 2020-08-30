using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseService.Migrations
{
    public partial class update_UserJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_base_user_jobs",
                table: "base_user_jobs");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "base_user_jobs");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "base_user_jobs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_base_user_jobs",
                table: "base_user_jobs",
                columns: new[] { "UserId", "JobId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_base_user_jobs",
                table: "base_user_jobs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "base_user_jobs");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "base_user_jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_base_user_jobs",
                table: "base_user_jobs",
                columns: new[] { "EmployeeId", "JobId" });
        }
    }
}
