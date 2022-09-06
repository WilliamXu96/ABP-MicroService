using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseService.Migrations
{
    public partial class update_Menu_Path : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Route",
                table: "base_menu",
                newName: "Path");

            migrationBuilder.AddColumn<string>(
                name: "Component",
                table: "base_menu",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Component",
                table: "base_menu");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "base_menu",
                newName: "Route");
        }
    }
}
