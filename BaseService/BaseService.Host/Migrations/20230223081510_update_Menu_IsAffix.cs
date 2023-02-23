using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseService.Migrations
{
    /// <inheritdoc />
    public partial class updateMenuIsAffix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAffix",
                table: "base_menu",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAffix",
                table: "base_menu");
        }
    }
}
