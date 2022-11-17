using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFinder.Migrations
{
    public partial class AdminAccountAangepast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccb26c1-a558-4b56-8f1a-5a2735a7d862");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyID", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5c27080e-3ed3-4719-9b7a-be25789d7bd9", 0, 1, "549d98fe-a452-40f5-89b6-653a7334777b", "pietjan@gmail.com", true, "Piet", "Jan", false, null, "PIETJAN@GMAIL.COM", "PIETJAN", "AQAAAAEAACcQAAAAENAFiktJ7nBF/e7M+Zn9WeFVPzxxCqZV61klymcTqSoLCkMq1ftzmt/FENhJIIfyNQ==", null, false, "9b31d8c8-7227-4d6d-9ec3-1009ad26cb1b", false, "PietJan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5c27080e-3ed3-4719-9b7a-be25789d7bd9");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyID", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9ccb26c1-a558-4b56-8f1a-5a2735a7d862", 0, 1, "34345809-ea07-4a06-a075-c9dd286b9cae", "pietjan@gmail.com", false, "Piet", "Jan", false, null, null, null, "AQAAAAEAACcQAAAAEGA2HGVrHRZ1A8dfAZkSegywY0YilSa8yPEwhRpBbC/vLnws+o8EvmsCeIyJW5FsRg==", null, false, "846b2a80-abfe-4cf5-9614-85ff1b248813", false, "PietJan" });
        }
    }
}
