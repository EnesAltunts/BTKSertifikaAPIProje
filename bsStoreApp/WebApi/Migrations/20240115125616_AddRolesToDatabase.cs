using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddRolesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ccd0bf07-8952-49c3-8cfb-2e47e40a8290", "b4bebe23-4f2a-467c-9a90-531e2f9368d4", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "651da53b-84be-4833-a9c6-c566fa93cdba", "a53fbe09-e1f5-4e3e-a9cb-eeafa4a2b263", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e29c7721-143d-4a8d-af83-1be0d81a0f3c", "71bb7c7e-86b1-4a37-b435-2d380df8020d", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "651da53b-84be-4833-a9c6-c566fa93cdba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccd0bf07-8952-49c3-8cfb-2e47e40a8290");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e29c7721-143d-4a8d-af83-1be0d81a0f3c");
        }
    }
}
