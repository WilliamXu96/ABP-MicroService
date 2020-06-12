using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class update_org_add_haschildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "base_orgs");

            migrationBuilder.AddColumn<bool>(
                name: "HasChildren",
                table: "base_orgs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasChildren",
                table: "base_orgs");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "base_orgs",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }
    }
}
