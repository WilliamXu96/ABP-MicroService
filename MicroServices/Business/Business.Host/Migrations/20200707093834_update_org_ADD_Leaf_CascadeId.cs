using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class update_org_ADD_Leaf_CascadeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CascadeId",
                table: "base_orgs",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Leaf",
                table: "base_orgs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CascadeId",
                table: "base_orgs");

            migrationBuilder.DropColumn(
                name: "Leaf",
                table: "base_orgs");
        }
    }
}
