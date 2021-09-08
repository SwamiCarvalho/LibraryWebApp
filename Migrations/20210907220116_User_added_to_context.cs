using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class User_added_to_context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "009a71f4-df4f-41ac-a2c5-40cb61f77dfd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18b42120-34ca-4d06-af6e-b99d3af9f31e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cfddb30-7800-4f7d-96ef-c4104302809b");

            migrationBuilder.CreateTable(
                name: "Librarian",
                columns: table => new
                {
                    LibrarianId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librarian", x => x.LibrarianId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4285f7a7-5fd8-46c2-8638-91924362b310", "a336a14f-8f5e-409e-9185-7be03b85d2f2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e90d7bdd-f3c8-4c67-af10-f0035a76ddb1", "d460cfb8-0552-433f-bb20-13ed3254759f", "Reader", "READER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28aeb7d6-4332-4f24-8253-55b9bea4c63e", "d72c6073-b000-499b-abc6-0c4c9af16c63", "Librarian", "LIBRARIAN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Librarian");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28aeb7d6-4332-4f24-8253-55b9bea4c63e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4285f7a7-5fd8-46c2-8638-91924362b310");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e90d7bdd-f3c8-4c67-af10-f0035a76ddb1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9cfddb30-7800-4f7d-96ef-c4104302809b", "9693b32a-f47c-4dd3-b00d-1737fd7ac3ac", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "18b42120-34ca-4d06-af6e-b99d3af9f31e", "57a68afa-8f4f-4dde-a22d-3df7cf887aff", "Reader", "READER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "009a71f4-df4f-41ac-a2c5-40cb61f77dfd", "b3825d98-a5fa-4eb7-a5bc-28e5e3988cf7", "Librarian", "LIBRARIAN" });
        }
    }
}
