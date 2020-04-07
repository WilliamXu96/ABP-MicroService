using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class create_dictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dictionary",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    FullName = table.Column<string>(maxLength: 256, nullable: false),
                    CategoryID = table.Column<Guid>(nullable: true),
                    Notes = table.Column<string>(maxLength: 200, nullable: true),
                    PID = table.Column<Guid>(nullable: true),
                    SEQ = table.Column<int>(nullable: false),
                    IsEdit = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictionary", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dictionary_Code",
                table: "dictionary",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dictionary");
        }
    }
}
