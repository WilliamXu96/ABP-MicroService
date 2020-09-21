using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSystem.Migrations
{
    public partial class update_fileInfo_size : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "file_info",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "file_info",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
