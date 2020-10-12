using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class Init_Form : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "base_form",
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
                    Api = table.Column<string>(maxLength: 200, nullable: false),
                    FormName = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Disabled = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_form", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "base_form_fields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    FormId = table.Column<Guid>(nullable: false),
                    FieldType = table.Column<string>(maxLength: 50, nullable: false),
                    FieldName = table.Column<string>(maxLength: 50, nullable: false),
                    Label = table.Column<string>(maxLength: 128, nullable: false),
                    Placeholder = table.Column<string>(maxLength: 50, nullable: true),
                    DefaultValue = table.Column<string>(maxLength: 256, nullable: true),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    Maxlength = table.Column<int>(nullable: true),
                    IsReadonly = table.Column<bool>(nullable: false),
                    IsRequire = table.Column<bool>(nullable: false),
                    IsSort = table.Column<bool>(nullable: false),
                    Disabled = table.Column<bool>(nullable: false),
                    Regx = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Options = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_form_fields", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "base_form");

            migrationBuilder.DropTable(
                name: "base_form_fields");
        }
    }
}
