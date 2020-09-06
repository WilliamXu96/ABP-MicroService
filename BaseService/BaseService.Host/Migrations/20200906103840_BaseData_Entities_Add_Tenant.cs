using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseService.Migrations
{
    public partial class BaseData_Entities_Add_Tenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "base_user_jobs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "base_orgs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "base_jobs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "base_dict_details",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "base_dict",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "base_user_jobs");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "base_orgs");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "base_jobs");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "base_dict_details");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "base_dict");
        }
    }
}
