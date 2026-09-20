using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Hrms.Migrations
{
    /// <inheritdoc />
    public partial class changesDatatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experinces_Designations_DesignationId",
                table: "Experinces");

            migrationBuilder.DropIndex(
                name: "IX_Experinces_DesignationId",
                table: "Experinces");

            migrationBuilder.DropColumn(
                name: "DesignationId",
                table: "Experinces");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Experinces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Experinces");

            migrationBuilder.AddColumn<int>(
                name: "DesignationId",
                table: "Experinces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Experinces_DesignationId",
                table: "Experinces",
                column: "DesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experinces_Designations_DesignationId",
                table: "Experinces",
                column: "DesignationId",
                principalTable: "Designations",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
