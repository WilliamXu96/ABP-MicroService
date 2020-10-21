using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class update_FormField_IsRequire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequire",
                table: "base_form_fields");

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "base_form_fields",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "base_form_fields");

            migrationBuilder.AddColumn<bool>(
                name: "IsRequire",
                table: "base_form_fields",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
