using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Hrms.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTerminationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resignation_Departments_DepartmentId",
                table: "Resignation");

            migrationBuilder.DropForeignKey(
                name: "FK_Resignation_Users_UserId",
                table: "Resignation");

            migrationBuilder.CreateTable(
                name: "Termination",
                columns: table => new
                {
                    TerminationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TerminationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoticeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResignDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termination", x => x.TerminationId);
                    table.ForeignKey(
                        name: "FK_Termination_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Termination_UserId",
                table: "Termination",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resignation_Departments_DepartmentId",
                table: "Resignation",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resignation_Users_UserId",
                table: "Resignation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resignation_Departments_DepartmentId",
                table: "Resignation");

            migrationBuilder.DropForeignKey(
                name: "FK_Resignation_Users_UserId",
                table: "Resignation");

            migrationBuilder.DropTable(
                name: "Termination");

            migrationBuilder.AddForeignKey(
                name: "FK_Resignation_Departments_DepartmentId",
                table: "Resignation",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resignation_Users_UserId",
                table: "Resignation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
