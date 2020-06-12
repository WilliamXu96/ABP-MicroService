using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class update_org_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enable",
                table: "base_orgs");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "base_orgs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "base_orgs");

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                table: "base_orgs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
