using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTimelyIn.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workHours_Employees_EmployeeId",
                table: "workHours");

            migrationBuilder.DropTable(
                name: "VacationRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workHours",
                table: "workHours");

            migrationBuilder.RenameTable(
                name: "workHours",
                newName: "WorkHours");

            migrationBuilder.RenameIndex(
                name: "IX_workHours_EmployeeId",
                table: "WorkHours",
                newName: "IX_WorkHours_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkHours",
                table: "WorkHours",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_EmployeeId",
                table: "Vacations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHours_Employees_EmployeeId",
                table: "WorkHours",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkHours_Employees_EmployeeId",
                table: "WorkHours");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkHours",
                table: "WorkHours");

            migrationBuilder.RenameTable(
                name: "WorkHours",
                newName: "workHours");

            migrationBuilder.RenameIndex(
                name: "IX_WorkHours_EmployeeId",
                table: "workHours",
                newName: "IX_workHours_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workHours",
                table: "workHours",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "VacationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_EmployeeId",
                table: "VacationRequests",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_workHours_Employees_EmployeeId",
                table: "workHours",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
