using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Hrms.Migrations
{
    /// <inheritdoc />
    public partial class PayslipUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnings",
                table: "Payslips",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDeductions",
                table: "Payslips",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PayslipPath",
                table: "Payslips",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NetSalary",
                table: "Payslips",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HourlyRate",
                table: "Payslips",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationAddress",
                table: "Payslips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrganizationEmail",
                table: "Payslips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrganizationName",
                table: "Payslips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrganizationPhone",
                table: "Payslips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalHoursInMonth",
                table: "Payslips",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WorkedHours",
                table: "Payslips",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PayslipsPayslipId",
                table: "Earnings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayslipsPayslipId",
                table: "Deductions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Earnings_PayslipsPayslipId",
                table: "Earnings",
                column: "PayslipsPayslipId");

            migrationBuilder.CreateIndex(
                name: "IX_Deductions_PayslipsPayslipId",
                table: "Deductions",
                column: "PayslipsPayslipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deductions_Payslips_PayslipsPayslipId",
                table: "Deductions",
                column: "PayslipsPayslipId",
                principalTable: "Payslips",
                principalColumn: "PayslipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Earnings_Payslips_PayslipsPayslipId",
                table: "Earnings",
                column: "PayslipsPayslipId",
                principalTable: "Payslips",
                principalColumn: "PayslipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deductions_Payslips_PayslipsPayslipId",
                table: "Deductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Earnings_Payslips_PayslipsPayslipId",
                table: "Earnings");

            migrationBuilder.DropIndex(
                name: "IX_Earnings_PayslipsPayslipId",
                table: "Earnings");

            migrationBuilder.DropIndex(
                name: "IX_Deductions_PayslipsPayslipId",
                table: "Deductions");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "OrganizationAddress",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "OrganizationEmail",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "OrganizationName",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "OrganizationPhone",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "TotalHoursInMonth",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "WorkedHours",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "PayslipsPayslipId",
                table: "Earnings");

            migrationBuilder.DropColumn(
                name: "PayslipsPayslipId",
                table: "Deductions");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnings",
                table: "Payslips",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDeductions",
                table: "Payslips",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PayslipPath",
                table: "Payslips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NetSalary",
                table: "Payslips",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");
        }
    }
}
