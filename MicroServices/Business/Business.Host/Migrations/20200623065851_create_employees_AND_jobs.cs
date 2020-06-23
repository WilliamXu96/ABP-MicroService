using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class create_employees_AND_jobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "base_orgs",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateTable(
                name: "base_employee_jobs",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    JobId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_employee_jobs", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "base_employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<short>(nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    OrgId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "base_jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_jobs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_base_employees_UserId",
                table: "base_employees",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "base_employee_jobs");

            migrationBuilder.DropTable(
                name: "base_employees");

            migrationBuilder.DropTable(
                name: "base_jobs");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "base_orgs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }
    }
}
