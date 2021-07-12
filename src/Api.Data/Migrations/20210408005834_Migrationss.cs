using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Migrationss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "UpdateAt" },
                values: new object[] { new Guid("43baf655-f722-4818-9552-5027ab86e0f1"), new DateTime(2021, 4, 7, 21, 58, 34, 661, DateTimeKind.Local).AddTicks(7767), "admin@admin.com", "Administrador", new DateTime(2021, 4, 7, 21, 58, 34, 662, DateTimeKind.Local).AddTicks(6280) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("43baf655-f722-4818-9552-5027ab86e0f1"));
        }
    }
}
