using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Hrms.Migrations
{
    /// <inheritdoc />
    public partial class addProjectsinuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
    name: "UserId",
    table: "Projects",
    type: "int",
    nullable: true);

            migrationBuilder.AddForeignKey(
    name: "FK_Projects_Users_UserId",
    table: "Projects",
    column: "UserId",
    principalTable: "Users",
    principalColumn: "UserId",
    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                 name: "FK_Projects_Users_UserId",
                 table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Projects");
        }
    }
}
