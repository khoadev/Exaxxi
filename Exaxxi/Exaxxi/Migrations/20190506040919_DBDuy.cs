using Microsoft.EntityFrameworkCore.Migrations;

namespace Exaxxi.Migrations
{
    public partial class DBDuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Users",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
