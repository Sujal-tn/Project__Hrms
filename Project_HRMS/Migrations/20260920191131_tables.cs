using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Hrms.Migrations
{
    /// <inheritdoc />
    public partial class tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId2",
                table: "FamilyInformations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId2",
                table: "Experinces",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId2",
                table: "EductionDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId2",
                table: "BankInformations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyInformations_UserId2",
                table: "FamilyInformations",
                column: "UserId2",
                unique: true,
                filter: "[UserId2] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Experinces_UserId2",
                table: "Experinces",
                column: "UserId2",
                unique: true,
                filter: "[UserId2] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EductionDetails_UserId2",
                table: "EductionDetails",
                column: "UserId2",
                unique: true,
                filter: "[UserId2] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BankInformations_UserId2",
                table: "BankInformations",
                column: "UserId2",
                unique: true,
                filter: "[UserId2] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BankInformations_Users_UserId2",
                table: "BankInformations",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EductionDetails_Users_UserId2",
                table: "EductionDetails",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experinces_Users_UserId2",
                table: "Experinces",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyInformations_Users_UserId2",
                table: "FamilyInformations",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankInformations_Users_UserId2",
                table: "BankInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_EductionDetails_Users_UserId2",
                table: "EductionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Experinces_Users_UserId2",
                table: "Experinces");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyInformations_Users_UserId2",
                table: "FamilyInformations");

            migrationBuilder.DropIndex(
                name: "IX_FamilyInformations_UserId2",
                table: "FamilyInformations");

            migrationBuilder.DropIndex(
                name: "IX_Experinces_UserId2",
                table: "Experinces");

            migrationBuilder.DropIndex(
                name: "IX_EductionDetails_UserId2",
                table: "EductionDetails");

            migrationBuilder.DropIndex(
                name: "IX_BankInformations_UserId2",
                table: "BankInformations");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "FamilyInformations");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "Experinces");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "EductionDetails");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "BankInformations");
        }
    }
}
