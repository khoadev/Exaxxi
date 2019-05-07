using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exaxxi.Migrations
{
    public partial class addDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(maxLength: 30, nullable: false),
                    email = table.Column<string>(nullable: false),
                    level = table.Column<int>(nullable: false),
                    date_create = table.Column<DateTime>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    active = table.Column<bool>(nullable: false),
                    order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ds_Size",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VN = table.Column<int>(nullable: false),
                    US = table.Column<float>(nullable: false),
                    UK = table.Column<float>(nullable: false),
                    Inch = table.Column<float>(nullable: false),
                    Centimet = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ds_Size", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    active = table.Column<bool>(nullable: false),
                    order = table.Column<int>(nullable: false),
                    id_department = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.id);
                    table.ForeignKey(
                        name: "FK_Brands_Departments_id_department",
                        column: x => x.id_department,
                        principalTable: "Departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    content = table.Column<string>(nullable: false),
                    date_create = table.Column<DateTime>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    id_admin = table.Column<int>(nullable: false),
                    id_department = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.id);
                    table.ForeignKey(
                        name: "FK_News_Admins_id_admin",
                        column: x => x.id_admin,
                        principalTable: "Admins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_Departments_id_department",
                        column: x => x.id_department,
                        principalTable: "Departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(maxLength: 30, nullable: false),
                    confirm_password = table.Column<string>(maxLength: 30, nullable: false),
                    email = table.Column<string>(nullable: false),
                    shoe_sizeid = table.Column<int>(nullable: true),
                    level_seller = table.Column<int>(nullable: false),
                    score_buyer = table.Column<int>(nullable: false),
                    date_registion = table.Column<DateTime>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_ds_Size_shoe_sizeid",
                        column: x => x.shoe_sizeid,
                        principalTable: "ds_Size",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    active = table.Column<bool>(nullable: false),
                    order = table.Column<int>(nullable: false),
                    id_brand = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_Categories_Brands_id_brand",
                        column: x => x.id_brand,
                        principalTable: "Brands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    info = table.Column<string>(nullable: true),
                    img = table.Column<string>(nullable: false),
                    volatility = table.Column<float>(nullable: false),
                    trade_min = table.Column<double>(nullable: false),
                    trade_max = table.Column<double>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    id_admin = table.Column<int>(nullable: false),
                    id_category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.id);
                    table.ForeignKey(
                        name: "FK_Items_Admins_id_admin",
                        column: x => x.id_admin,
                        principalTable: "Admins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Categories_id_category",
                        column: x => x.id_category,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    lowest_ask = table.Column<double>(nullable: false),
                    highest_bid = table.Column<double>(nullable: false),
                    last_sale = table.Column<double>(nullable: false),
                    id_ds_size = table.Column<int>(nullable: false),
                    id_item = table.Column<int>(nullable: false),
                    Adminsid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Sizes_Admins_Adminsid",
                        column: x => x.Adminsid,
                        principalTable: "Admins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sizes_ds_Size_id_ds_size",
                        column: x => x.id_ds_size,
                        principalTable: "ds_Size",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sizes_Items_id_item",
                        column: x => x.id_item,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Followings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_user = table.Column<int>(nullable: false),
                    id_size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followings", x => x.id);
                    table.ForeignKey(
                        name: "FK_Followings_Sizes_id_size",
                        column: x => x.id_size,
                        principalTable: "Sizes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Followings_Users_id_user",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    price = table.Column<double>(nullable: false),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: false),
                    kind = table.Column<int>(nullable: false),
                    id_size = table.Column<int>(nullable: false),
                    id_user = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Posts_Sizes_id_size",
                        column: x => x.id_size,
                        principalTable: "Sizes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Users_id_user",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_id_department",
                table: "Brands",
                column: "id_department");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_id_brand",
                table: "Categories",
                column: "id_brand");

            migrationBuilder.CreateIndex(
                name: "IX_Followings_id_size",
                table: "Followings",
                column: "id_size");

            migrationBuilder.CreateIndex(
                name: "IX_Followings_id_user",
                table: "Followings",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Items_id_admin",
                table: "Items",
                column: "id_admin");

            migrationBuilder.CreateIndex(
                name: "IX_Items_id_category",
                table: "Items",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "IX_News_id_admin",
                table: "News",
                column: "id_admin");

            migrationBuilder.CreateIndex(
                name: "IX_News_id_department",
                table: "News",
                column: "id_department");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_id_size",
                table: "Posts",
                column: "id_size");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_id_user",
                table: "Posts",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_Adminsid",
                table: "Sizes",
                column: "Adminsid");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_id_ds_size",
                table: "Sizes",
                column: "id_ds_size");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_id_item",
                table: "Sizes",
                column: "id_item");

            migrationBuilder.CreateIndex(
                name: "IX_Users_shoe_sizeid",
                table: "Users",
                column: "shoe_sizeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Followings");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ds_Size");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
