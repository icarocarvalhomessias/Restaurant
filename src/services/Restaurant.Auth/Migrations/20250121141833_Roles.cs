using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Auth.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                { Guid.NewGuid().ToString(), "Admin", "ADMIN", Guid.NewGuid().ToString() },
                { Guid.NewGuid().ToString(), "DeliveryMan", "DELIVERYMAN", Guid.NewGuid().ToString() }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Name",
                keyValues: new object[] { "Admin", "DeliveryMan" });
        }
    }
}
