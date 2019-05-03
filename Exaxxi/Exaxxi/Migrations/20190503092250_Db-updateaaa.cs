using Microsoft.EntityFrameworkCore.Migrations;

namespace Exaxxi.Migrations
{
    public partial class Dbupdateaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "News",
                newName: "vi_title");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "News",
                newName: "vi_content");

            migrationBuilder.RenameColumn(
                name: "info",
                table: "Items",
                newName: "vi_info");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Departments",
                newName: "vi_name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Categories",
                newName: "vi_name");

            migrationBuilder.AddColumn<string>(
                name: "en_content",
                table: "News",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "en_title",
                table: "News",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "en_info",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "en_name",
                table: "Departments",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "en_name",
                table: "Categories",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "en_content",
                table: "News");

            migrationBuilder.DropColumn(
                name: "en_title",
                table: "News");

            migrationBuilder.DropColumn(
                name: "en_info",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "en_name",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "en_name",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "vi_title",
                table: "News",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "vi_content",
                table: "News",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "vi_info",
                table: "Items",
                newName: "info");

            migrationBuilder.RenameColumn(
                name: "vi_name",
                table: "Departments",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "vi_name",
                table: "Categories",
                newName: "name");
        }
    }
}
