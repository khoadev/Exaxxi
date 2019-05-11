using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exaxxi.Migrations
{
    public partial class NewDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(nullable: false),
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
                    vi_name = table.Column<string>(maxLength: 50, nullable: true),
                    en_name = table.Column<string>(maxLength: 50, nullable: true),
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
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    level_seller = table.Column<int>(nullable: false),
                    score_buyer = table.Column<int>(nullable: false),
                    date_registion = table.Column<DateTime>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
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
                    vi_title = table.Column<string>(nullable: false),
                    en_title = table.Column<string>(nullable: false),
                    vi_content = table.Column<string>(nullable: false),
                    en_content = table.Column<string>(nullable: false),
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
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    vi_name = table.Column<string>(maxLength: 50, nullable: false),
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
                    vi_info = table.Column<string>(nullable: true),
                    en_info = table.Column<string>(nullable: true),
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

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "id", "active", "date_create", "email", "level", "name", "password" },
                values: new object[,]
                {
                    { 1, true, new DateTime(1969, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "genos@gmail.com", 1, "genos", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413" },
                    { 2, true, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "duytran@gmail.com", 1, "duytran", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413" },
                    { 3, true, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoado@gmail.com", 1, "khoado", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413" },
                    { 4, true, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "kietnguyen@gmail.com", 1, "kietnguyen", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "id", "active", "en_name", "order", "vi_name" },
                values: new object[,]
                {
                    { 1, true, "Sneaker", 1, "Giày" },
                    { 2, true, "StreetWear", 2, "Quần Áo" },
                    { 3, true, "Watch", 3, "Đồng Hồ" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "active", "date_registion", "email", "level_seller", "name", "password", "score_buyer" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "teo@gmail.com", 1, "Lê văn tèo", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413", 1 },
                    { 2, true, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ty@gmail.com", 2, "Lê văn tý", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413", 2 },
                    { 3, true, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "tun@gmail.com", 3, "Lê văn tun", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413", 3 },
                    { 4, true, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "tet@gmail.com", 4, "Lê văn tet", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413", 4 }
                });

            migrationBuilder.InsertData(
                table: "ds_Size",
                columns: new[] { "id", "Centimet", "Inch", "UK", "US", "VN" },
                values: new object[,]
                {
                    { 1, 45f, 13f, 20f, 25f, 30 },
                    { 2, 50f, 18f, 25f, 30f, 35 },
                    { 3, 55f, 21f, 30f, 35f, 40 }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "id", "active", "id_department", "name", "order" },
                values: new object[,]
                {
                    { 1, true, 1, "AdidasPho", 1 },
                    { 2, true, 1, "Nikes", 2 },
                    { 3, true, 1, "Bittis", 3 },
                    { 4, true, 2, "AoDaiViet", 4 },
                    { 5, true, 2, "QuanJeanGenos", 5 },
                    { 6, true, 2, "NonLa", 6 },
                    { 7, true, 3, "Citizen", 7 },
                    { 8, true, 3, "Omega", 8 },
                    { 9, true, 3, "Rolex", 9 }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "id", "active", "date_create", "en_content", "en_title", "id_admin", "id_department", "vi_content", "vi_title" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2019, 5, 10, 21, 13, 26, 224, DateTimeKind.Local), "Shoe is producted in pair now, it look so beautyful", "News about shoes", 1, 1, "Giày vừa được cho sản xuất thành 2 chiếc 1 đôi, đeo vô nhìn rắt là đẹp", "Tin tức về Giày" },
                    { 2, true, new DateTime(2019, 5, 10, 21, 13, 26, 225, DateTimeKind.Local), "Shoe must be wore by pair, it's so stupid ", "Shoe is put on by pair", 2, 1, "Người ta đã bắt đeo giày theo cặp không cho đeo theo chiếc nữa", "Giày đeo theo cặp" },
                    { 4, true, new DateTime(2019, 5, 10, 21, 13, 26, 225, DateTimeKind.Local), "Shoe must be wore by pair, it's so stupid ", "Shoe is put on by pair", 2, 1, "Người ta đã bắt đeo giày theo cặp không cho đeo theo chiếc nữa", "Giày đeo theo cặp" },
                    { 3, true, new DateTime(2019, 5, 10, 21, 13, 26, 225, DateTimeKind.Local), "Genos is so handsome, he hold all marketplace is tangibility ", "Clothes of Genos Brand is hold 100% marketplace of Asia", 3, 2, "Genos đẹp trai nên nắm giữ toàn bộ thị trường đúng rồi khỏi thắc mắc gì thêm", "Quần Áo Hiệu Genos đọc chiếm thị trường Châu Á" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "id", "active", "id_brand", "order", "vi_name" },
                values: new object[,]
                {
                    { 1, true, 1, 1, "Yezzze" },
                    { 25, true, 9, 25, "Rolex I" },
                    { 24, true, 8, 24, "Ome C" },
                    { 23, true, 8, 23, "Ome B" },
                    { 22, true, 8, 22, "Ome A" },
                    { 21, true, 7, 21, "Citi W3" },
                    { 20, true, 7, 20, "Citi W2" },
                    { 19, true, 7, 19, "Citi W1" },
                    { 18, true, 6, 18, "Nón Lá BêTong" },
                    { 17, true, 6, 17, "Nón Lá Thép" },
                    { 16, true, 6, 16, "Nón Lá làm từ lá" },
                    { 15, true, 5, 15, "Quần Jean Hở Hang" },
                    { 26, true, 9, 26, "Rolex II" },
                    { 14, true, 5, 14, "Quần Jean Sexy" },
                    { 12, true, 4, 12, "Áo Dài Sexy" },
                    { 11, true, 4, 11, "Áo Dài Kín Đáo" },
                    { 10, true, 4, 10, "Áo Dài Hở Hang" },
                    { 9, true, 3, 9, "Bitit x Hunter" },
                    { 8, true, 3, 8, "Hunter x Hunter" },
                    { 7, true, 3, 7, "Hunter" },
                    { 6, true, 2, 6, "Kobe" },
                    { 5, true, 2, 5, "AirMax" },
                    { 4, true, 2, 4, "AirForce" },
                    { 3, true, 1, 3, "NMD" },
                    { 2, true, 1, 2, "Ultraboost" },
                    { 13, true, 5, 13, "Quần Jean Lửng" },
                    { 27, true, 9, 27, "Rolex III" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "active", "en_info", "id_admin", "id_category", "img", "name", "trade_max", "trade_min", "vi_info", "volatility" },
                values: new object[,]
                {
                    { 1, false, "There are some infos about Item 1", 1, 1, "item1.pnj", "Item 1", 200.0, 100.0, "Thông Tin của item 1 sẽ hiện ở đây", 0f },
                    { 2, false, "There are some infos about Item 2", 1, 1, "item2.pnj", "Item 2", 200.0, 100.0, "Thông Tin của item 2 sẽ hiện ở đây", 0f },
                    { 3, false, "There are some infos about Item 3", 1, 1, "item3.pnj", "Item 3", 200.0, 100.0, "Thông Tin của item 3 sẽ hiện ở đây", 0f },
                    { 4, false, "There are some infos about Item I", 1, 1, "item3.pnj", "Item I", 200.0, 100.0, "Thông Tin của item I sẽ hiện ở đây", 0f },
                    { 5, false, "There are some infos about Item II", 1, 1, "item3.pnj", "Item II", 200.0, 100.0, "Thông Tin của item II sẽ hiện ở đây", 0f },
                    { 6, false, "There are some infos about Item III", 1, 1, "item3.pnj", "Item III", 200.0, 100.0, "Thông Tin của item III sẽ hiện ở đây", 0f },
                    { 7, false, "There are some infos about Item X", 1, 1, "item3.pnj", "Item X", 200.0, 100.0, "Thông Tin của item X sẽ hiện ở đây", 0f },
                    { 16, false, "There are some infos about Item X1", 1, 1, "item3.pnj", "Item X1", 200.0, 100.0, "Thông Tin của item X1 sẽ hiện ở đây", 0f },
                    { 8, false, "There are some infos about Item X12", 1, 1, "item3.pnj", "Item X12", 200.0, 100.0, "Thông Tin của item X12 sẽ hiện ở đây", 0f },
                    { 9, false, "There are some infos about Item X121", 1, 1, "item3.pnj", "Item X121", 200.0, 100.0, "Thông Tin của item X121 sẽ hiện ở đây", 0f },
                    { 10, false, "There are some infos about Item XEEeEEEEEEEeeeE", 1, 1, "item3.pnj", "Item XEEEEEEE", 200.0, 100.0, "Thông Tin của item XEEêEEêEE sẽ hiện ở đây", 0f },
                    { 11, false, "There are some infos about Item XEEeEEEEEEEeeeE", 1, 2, "item3.pnj", "Item XEEEEEEE", 200.0, 100.0, "Thông Tin của item XEEêEEêEE sẽ hiện ở đây", 0f },
                    { 12, false, "There are some infos about Item XEEEEádasdasfsafasE", 1, 2, "item3.pnj", "Item XEEEEádasdasfsafasE ", 200.0, 100.0, "Thông Tin của item XEEEEádasdasfsafasE sẽ hiện ở đây", 0f },
                    { 13, false, "There are some infos about Item qưtegdsdgegwggegw", 1, 2, "item3.pnj", "Item qưtegdsdgegwggegw ", 200.0, 100.0, "Thông Tin của item qưtegdsdgegwggegw sẽ hiện ở đây", 0f },
                    { 14, false, "There are some infos about Item fasfasfasfsafsafsaf", 1, 2, "item3.pnj", "fasfasfasfsafsafsaf ", 200.0, 100.0, "Thông Tin của item fasfasfasfsafsafsaf sẽ hiện ở đây", 0f },
                    { 15, false, "There are some infos about Item fasfasfasfsafsafsaf", 1, 2, "item3.pnj", "fasfasfasfsafsafsaf ", 200.0, 100.0, "Thông Tin của item fasfasfasfsafsafsaf sẽ hiện ở đây", 0f }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "id", "Adminsid", "highest_bid", "id_ds_size", "id_item", "last_sale", "lowest_ask" },
                values: new object[,]
                {
                    { 1, null, 25.0, 1, 1, 24.0, 30.0 },
                    { 2, null, 35.0, 2, 2, 29.0, 40.0 },
                    { 3, null, 35.0, 2, 2, 29.0, 40.0 },
                    { 4, null, 45.0, 3, 3, 40.0, 55.0 },
                    { 5, null, 55.0, 1, 3, 45.0, 65.0 },
                    { 6, null, 33.0, 2, 3, 22.0, 44.0 }
                });

            migrationBuilder.InsertData(
                table: "Followings",
                columns: new[] { "id", "id_size", "id_user" },
                values: new object[,]
                {
                    { 2, 1, 1 },
                    { 3, 1, 2 },
                    { 1, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "id", "date_end", "date_start", "id_size", "id_user", "kind", "price" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1, 20.0 },
                    { 4, new DateTime(2019, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 2, 35.0 },
                    { 2, new DateTime(2019, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 2, 30.0 },
                    { 5, new DateTime(2019, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 1, 35.0 },
                    { 3, new DateTime(2019, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3, 2, 35.0 },
                    { 6, new DateTime(2019, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 1, 35.0 }
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
                name: "ds_Size");

            migrationBuilder.DropTable(
                name: "Items");

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
