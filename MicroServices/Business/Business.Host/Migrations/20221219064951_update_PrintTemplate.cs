using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business.Migrations
{
    public partial class update_PrintTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Orientation",
                table: "base_print_template",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperHeight",
                table: "base_print_template",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaperKind",
                table: "base_print_template",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperWidth",
                table: "base_print_template",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orientation",
                table: "base_print_template");

            migrationBuilder.DropColumn(
                name: "PaperHeight",
                table: "base_print_template");

            migrationBuilder.DropColumn(
                name: "PaperKind",
                table: "base_print_template");

            migrationBuilder.DropColumn(
                name: "PaperWidth",
                table: "base_print_template");
        }
    }
}
