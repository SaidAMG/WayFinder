using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WayFinder.Migrations
{
    public partial class DbContextAddLevelLocationCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48ceb691-1539-48da-bf5b-0e03b24bff2c");

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "ID", "LevelNumber" },
                values: new object[] { 1, 0 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "ID", "Description", "LevelID", "Name", "X", "Y" },
                values: new object[] { 1, "Kantoor", 1, "Ruimte 001", 1, 1 });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "LocationID", "Name" },
                values: new object[] { 1, 1, "Brightlands" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyID", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9ccb26c1-a558-4b56-8f1a-5a2735a7d862", 0, 1, "34345809-ea07-4a06-a075-c9dd286b9cae", "pietjan@gmail.com", false, "Piet", "Jan", false, null, null, null, "AQAAAAEAACcQAAAAEGA2HGVrHRZ1A8dfAZkSegywY0YilSa8yPEwhRpBbC/vLnws+o8EvmsCeIyJW5FsRg==", null, false, "846b2a80-abfe-4cf5-9614-85ff1b248813", false, "PietJan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccb26c1-a558-4b56-8f1a-5a2735a7d862");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyID", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "48ceb691-1539-48da-bf5b-0e03b24bff2c", 0, null, "a65e5014-c72e-49af-ba49-6113ffad243e", "pietjan@gmail.com", false, "Piet", "Jan", false, null, null, null, "AQAAAAEAACcQAAAAEGTN5WTjRa5/HKW6cw7juvquQaYgpE5uXeqQAjDItXdjRFyKnlsvBWbekn1iLmv5Vg==", null, false, "f807c236-3af3-41b2-81b7-e949a656b3cc", false, "PietJan" });
        }
    }
}
