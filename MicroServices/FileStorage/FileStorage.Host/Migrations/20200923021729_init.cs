using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileStorage.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "file_info",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    RealName = table.Column<string>(maxLength: 64, nullable: false),
                    Suffix = table.Column<string>(maxLength: 50, nullable: false),
                    Md5Code = table.Column<string>(maxLength: 256, nullable: false),
                    Size = table.Column<string>(maxLength: 100, nullable: false),
                    Path = table.Column<string>(maxLength: 256, nullable: false),
                    Url = table.Column<string>(maxLength: 256, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_info", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_info");
        }
    }
}
