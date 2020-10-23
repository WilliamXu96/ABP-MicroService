using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class update_FormField_TabelName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TabelName",
                table: "base_form");

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "base_form",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableName",
                table: "base_form");

            migrationBuilder.AddColumn<string>(
                name: "TabelName",
                table: "base_form",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
