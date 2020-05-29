using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class add_base_dict_AND_base_dict_details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dictionary",
                table: "dictionary");

            migrationBuilder.DropIndex(
                name: "IX_dictionary_Code",
                table: "dictionary");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "dictionary");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "dictionary");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "dictionary");

            migrationBuilder.DropColumn(
                name: "IsEdit",
                table: "dictionary");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "dictionary");

            migrationBuilder.DropColumn(
                name: "PID",
                table: "dictionary");

            migrationBuilder.DropColumn(
                name: "SEQ",
                table: "dictionary");

            migrationBuilder.RenameTable(
                name: "dictionary",
                newName: "base_dict");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "base_dict",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_base_dict",
                table: "base_dict",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "base_dict_details",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Pid = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(maxLength: 50, nullable: false),
                    Value = table.Column<string>(maxLength: 256, nullable: false),
                    Sort = table.Column<short>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_dict_details", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_base_dict_Name",
                table: "base_dict",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "base_dict_details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_base_dict",
                table: "base_dict");

            migrationBuilder.DropIndex(
                name: "IX_base_dict_Name",
                table: "base_dict");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "base_dict");

            migrationBuilder.RenameTable(
                name: "base_dict",
                newName: "dictionary");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryID",
                table: "dictionary",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "dictionary",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "dictionary",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsEdit",
                table: "dictionary",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "dictionary",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PID",
                table: "dictionary",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SEQ",
                table: "dictionary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_dictionary",
                table: "dictionary",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_dictionary_Code",
                table: "dictionary",
                column: "Code");
        }
    }
}
