using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticeApp.Data.Migrations
{
    public partial class SeedAppUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Name", "Email", "PhoneNo", "Role", "CreatedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { "Alice Smith", "alice@example.com", "555-0101", "Editor", DateTime.UtcNow.AddDays(-10), DateTime.UtcNow.AddDays(-5) },
                    { "Bob Johnson", "bob@example.com", "555-0102", "Viewer", DateTime.UtcNow.AddDays(-9), DateTime.UtcNow.AddDays(-4) },
                    { "Carol Williams", "carol@example.com", "555-0103", "Admin", DateTime.UtcNow.AddDays(-8), DateTime.UtcNow.AddDays(-3) },
                    { "David Brown", "david@example.com", "555-0104", "Editor", DateTime.UtcNow.AddDays(-7), DateTime.UtcNow.AddDays(-2) },
                    { "Eve Davis", "eve@example.com", "555-0105", "Viewer", DateTime.UtcNow.AddDays(-6), DateTime.UtcNow.AddDays(-1) },
                    { "Frank Miller", "frank@example.com", "555-0106", "Editor", DateTime.UtcNow.AddDays(-5), DateTime.UtcNow },
                    { "Grace Wilson", "grace@example.com", "555-0107", "Admin", DateTime.UtcNow.AddDays(-4), DateTime.UtcNow },
                    { "Hank Taylor", "hank@example.com", "555-0108", "Viewer", DateTime.UtcNow.AddDays(-3), DateTime.UtcNow },
                    { "Ivy Anderson", "ivy@example.com", "555-0109", "Editor", DateTime.UtcNow.AddDays(-2), DateTime.UtcNow },
                    { "Jack Thomas", "jack@example.com", "555-0110", "Viewer", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"AppUsers\" WHERE \"Email\" LIKE '%@example.com';");
        }
    }
}