using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class update_org : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "base_orgs");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "base_orgs");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "base_orgs");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "base_orgs");

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                table: "base_orgs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "base_orgs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enable",
                table: "base_orgs");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "base_orgs");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "base_orgs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "base_orgs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "base_orgs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "base_orgs",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);
        }
    }
}
