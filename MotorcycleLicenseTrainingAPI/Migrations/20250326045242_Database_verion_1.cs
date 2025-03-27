using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleLicenseTrainingAPI.Migrations
{
    public partial class Database_verion_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77c46877-537a-4637-bafe-b76b21a2f56e",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "87686594-8cd1-4b29-80c5-81693ef2d7c7", "3b124ef1-21c2-407d-b0ad-be9735d29e3c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77c46877-537a-4637-bafe-b76b21a2f56e",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e32d454f-ad57-4df0-bb96-64eb8dd823aa", "291f971b-3b88-4a27-80d9-2a6e4710b719" });
        }
    }
}
