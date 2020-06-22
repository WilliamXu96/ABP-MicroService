using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class update_orgs_del_hasChildren_leaf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasChildren",
                table: "base_orgs");

            migrationBuilder.DropColumn(
                name: "Leaf",
                table: "base_orgs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasChildren",
                table: "base_orgs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Leaf",
                table: "base_orgs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
