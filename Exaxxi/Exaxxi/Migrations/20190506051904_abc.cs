using Microsoft.EntityFrameworkCore.Migrations;

namespace Exaxxi.Migrations
{
    public partial class abc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ds_Size_shoe_sizeid",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_shoe_sizeid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "confirm_password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "shoe_sizeid",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "confirm_password",
                table: "Users",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "shoe_sizeid",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_shoe_sizeid",
                table: "Users",
                column: "shoe_sizeid");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ds_Size_shoe_sizeid",
                table: "Users",
                column: "shoe_sizeid",
                principalTable: "ds_Size",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
