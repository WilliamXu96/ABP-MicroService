using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class orgs_add_leaf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Leaf",
                table: "base_orgs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Leaf",
                table: "base_orgs");
        }
    }
}
