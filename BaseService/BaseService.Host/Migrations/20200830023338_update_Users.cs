using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseService.Migrations
{
    public partial class update_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "OrgId",
                table: "AbpUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "OrgId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
