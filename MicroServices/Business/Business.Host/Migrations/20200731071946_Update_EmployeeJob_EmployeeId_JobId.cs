using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class Update_EmployeeJob_EmployeeId_JobId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_base_employee_jobs",
                table: "base_employee_jobs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_base_employee_jobs",
                table: "base_employee_jobs",
                columns: new[] { "EmployeeId", "JobId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_base_employee_jobs",
                table: "base_employee_jobs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_base_employee_jobs",
                table: "base_employee_jobs",
                column: "JobId");
        }
    }
}
