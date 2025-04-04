using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DemoServerAppForHMAC.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ClientSecretKey = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ClientSecretSalt = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ClientInfos",
                columns: new[] { "Id", "ClientId", "ClientName", "ClientSecretKey", "ClientSecretSalt" },
                values: new object[,]
                {
                    { 1, "WebApp", "XYZ Company", "XYZCompany123", "Test" },
                    { 2, "MobileApp", "ABC Company", "XYZCompany123", "Test" },
                    { 3, "DeskTop", "123 Company", "XYZCompany123", "Test" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Salary", "UserName", "UserRole" },
                values: new object[,]
                {
                    { 1, 10000m, "Rama", "Software Engineer" },
                    { 2, 500000m, "Krishna", "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientInfos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
