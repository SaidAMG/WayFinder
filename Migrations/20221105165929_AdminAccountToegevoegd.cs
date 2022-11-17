using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFinder.Migrations
{
    public partial class AdminAccountToegevoegd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5c27080e-3ed3-4719-9b7a-be25789d7bd9");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyID", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3d568931-3294-44ef-8795-b844b91fa133", 0, 1, "dc17cd6f-f520-4e33-b970-3fa1ac5922da", "pietjan@gmail.com", true, "Piet", "Jan", false, null, "PIETJAN@GMAIL.COM", "PIETJAN", "AQAAAAEAACcQAAAAEPF4QmASvU7n8BSYSxOgbeNWBhutUbbLU/Dknj/EF6+uD1WwIzyRGg0kJC/8TD9VHQ==", null, false, "00d1a6fb-662b-4aeb-8b21-c96f3068c449", false, "PietJan" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "ID", "Description", "LevelID", "Name", "X", "Y" },
                values: new object[] { 2, "Kantoor", 1, "Ruimte 010", 5, 9 });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "LocationID", "Name" },
                values: new object[] { 2, 2, "Data4" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyID", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "29d2e3c7-b552-457a-85cd-f354ffceb55d", 0, 2, "99c56270-8caa-49d7-8678-ae2aa1395406", "jan.pillenman@gmail.com", true, "Jan", "Pillenman", false, null, "JAN.PILLENMAN@GMAIL.COM", "JANPILLENMAN", "AQAAAAEAACcQAAAAEOeH7z8pQPdzPmOsWoRsE38SUyPVCsSfHMdpgCTMsMaSoFVoyiCvTZM+vN8gbUqWKQ==", null, false, "69577667-19ad-45ad-9340-d7f96cc82453", false, "JanPillenman" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29d2e3c7-b552-457a-85cd-f354ffceb55d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d568931-3294-44ef-8795-b844b91fa133");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyID", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5c27080e-3ed3-4719-9b7a-be25789d7bd9", 0, 1, "549d98fe-a452-40f5-89b6-653a7334777b", "pietjan@gmail.com", true, "Piet", "Jan", false, null, "PIETJAN@GMAIL.COM", "PIETJAN", "AQAAAAEAACcQAAAAENAFiktJ7nBF/e7M+Zn9WeFVPzxxCqZV61klymcTqSoLCkMq1ftzmt/FENhJIIfyNQ==", null, false, "9b31d8c8-7227-4d6d-9ec3-1009ad26cb1b", false, "PietJan" });
        }
    }
}
