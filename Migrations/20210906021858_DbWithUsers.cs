using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class DbWithUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Reader_ReaderId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "NumberCC",
                table: "Reader");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "CCNumber",
                table: "Reader",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ReaderId",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Reader_ReaderId",
                table: "Bookings",
                column: "ReaderId",
                principalTable: "Reader",
                principalColumn: "ReaderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Reader_ReaderId",
                table: "Bookings");

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

            migrationBuilder.DropColumn(
                name: "CCNumber",
                table: "Reader");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Reader");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reader");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberCC",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "ReaderId",
                table: "Bookings",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Reader_ReaderId",
                table: "Bookings",
                column: "ReaderId",
                principalTable: "Reader",
                principalColumn: "ReaderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
