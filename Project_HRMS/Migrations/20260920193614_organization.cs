using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Hrms.Migrations
{
    /// <inheritdoc />
    public partial class organization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationLogo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
