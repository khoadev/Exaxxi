using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.Models
{
    public class ExaxxiDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<ds_Size> ds_Size { get; set; }
        public DbSet<Followings> Followings { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Sizes> Sizes { get; set; }

        //Server Name - Escanor: DESKTOP-9LUKN91\\SQLEXPRESS
        //Server Name - Khoa: DESKTOP-EH88R88\SQLEXPRESS
        //Server Name - Kiet: DESKTOP-262PU69\SQLEXPRESS
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-9LUKN91\SQLEXPRESS;Initial Catalog=Exaxxi;Integrated Security=True");


        }
        public ExaxxiDbContext(DbContextOptions<ExaxxiDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Admins>().HasData(
                new Admins
                {
                    id = 1,
                    name = "genos",
                    password = "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413",
                    email = "genos@gmail.com",
                    level = 1,
                    date_create = DateTime.Parse("1969-04-05"),
                    active = true,
                },
                new Admins
                {
                    id = 2,
                    name = "duytran",
                    password = "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413",
                    email = "duytran@gmail.com",
                    level = 1,
                    date_create = DateTime.Parse("2019-01-05"),
                    active = true,
                },
                new Admins
                {
                    id = 3,
                    name = "khoado",
                    password = "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413",
                    email = "khoado@gmail.com",
                    level = 1,
                    date_create = DateTime.Parse("2019-01-05"),
                    active = true,
                },
                new Admins
                {
                    id = 4,
                    name = "kietnguyen",
                    password = "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413",
                    email = "kietnguyen@gmail.com",
                    level = 1,
                    date_create = DateTime.Parse("2019-01-05"),
                    active = true,
                }
            );
            builder.Entity<Users>().HasData(
                new Users
                {
                    id = 1,
                    name = "Lê văn tèo",
                    email = "teo@gmail.com",
                    password = "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413",
                    level_seller = 1,
                    score_buyer = 1,
                    date_registion = DateTime.Parse("2019-01-05"),
                    active = true
                },
                new Users
                {
                    id = 2,
                    name = "Lê văn tý",
                    email = "ty@gmail.com",
                    password = "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413",
                    level_seller = 2,
                    score_buyer = 2,
                    date_registion = DateTime.Parse("2019-01-05"),
                    active = true
                },
                new Users
                {
                    id = 3,
                    name = "Lê văn tun",
                    email = "tun@gmail.com",
                    password = "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413",
                    level_seller = 3,
                    score_buyer = 3,
                    date_registion = DateTime.Parse("2019-01-05"),
                    active = true
                },
                new Users
                {
                    id = 4,
                    name = "Lê văn tet",
                    email = "tet@gmail.com",
                    password = "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413",
                    level_seller = 4,
                    score_buyer = 4,
                    date_registion = DateTime.Parse("2019-01-05"),
                    active = true
                }
            );
            builder.Entity<Departments>().HasData(
                new Departments
                {
                    vi_name = "Giày",
                    en_name = "Sneaker",
                    active = true,
                    order = 1,
                    id = 1
                },
                new Departments
                {
                    vi_name = "Quần Áo",
                    en_name = "StreetWear",
                    active = true,
                    order = 2,
                    id = 2
                },
                new Departments
                {
                    vi_name = "Đồng Hồ",
                    en_name = "Watch",
                    active = true,
                    order = 3,
                    id = 3
                }
            );
            builder.Entity<Brands>().HasData(
                new Brands
                {
                    id = 1,
                    name = "AdidasPho",
                    active = true,
                    order = 1,
                    id_department = 1,
                },
                new Brands
                {
                    id = 2,
                    name = "Nikes",
                    active = true,
                    order = 2,
                    id_department = 1,
                },
                new Brands
                {
                    id = 3,
                    name = "Bittis",
                    active = true,
                    order = 3,
                    id_department = 1,
                },
                new Brands
                {
                    id = 4,
                    name = "AoDaiViet",
                    active = true,
                    order = 4,
                    id_department = 2,
                },
                new Brands
                {

                    name = "QuanJeanGenos",
                    active = true,
                    order = 5,
                    id = 5,
                    id_department = 2,
                },
                new Brands
                {
                    name = "NonLa",
                    active = true,
                    order = 6,
                    id = 6,
                    id_department = 2,
                },
                new Brands
                {
                    name = "Citizen",
                    active = true,
                    order = 7,
                    id = 7,
                    id_department = 3,
                },
                new Brands
                {
                    name = "Omega",
                    active = true,
                    order = 8,
                    id = 8,
                    id_department = 3,
                },
                new Brands
                {
                    name = "Rolex",
                    active = true,
                    order = 9,
                    id = 9,
                    id_department = 3,
                }
            );
            builder.Entity<Categories>().HasData(
                new Categories
                {
                    vi_name = "Yezzze",
                    active = true,
                    order = 1,
                    id = 1,
                    id_brand = 1

                },
                new Categories
                {
                    vi_name = "Ultraboost",
                    active = true,
                    order = 2,
                    id = 2,
                    id_brand = 1

                },
                new Categories
                {
                    vi_name = "NMD",
                    active = true,
                    order = 3,
                    id = 3,
                    id_brand = 1

                },
                new Categories
                {
                    vi_name = "AirForce",
                    active = true,
                    order = 4,
                    id = 4,
                    id_brand = 2

                },
                new Categories
                {
                    vi_name = "AirMax",
                    active = true,
                    order = 5,
                    id = 5,
                    id_brand = 2

                },
                new Categories
                {
                    vi_name = "Kobe",
                    active = true,
                    order = 6,
                    id = 6,
                    id_brand = 2

                },
                new Categories
                {
                    vi_name = "Hunter",
                    active = true,
                    order = 7,
                    id = 7,
                    id_brand = 3

                },
                new Categories
                {
                    vi_name = "Hunter x Hunter",
                    active = true,
                    order = 8,
                    id = 8,
                    id_brand = 3

                },
                new Categories
                {
                    vi_name = "Bitit x Hunter",
                    active = true,
                    order = 9,
                    id = 9,
                    id_brand = 3

                },
                new Categories
                {
                    vi_name = "Áo Dài Hở Hang",
                    active = true,
                    order = 10,
                    id = 10,
                    id_brand = 4

                },
                new Categories
                {
                    vi_name = "Áo Dài Kín Đáo",
                    active = true,
                    order = 11,
                    id = 11,
                    id_brand = 4

                },
                new Categories
                {
                    vi_name = "Áo Dài Sexy",
                    active = true,
                    order = 12,
                    id = 12,
                    id_brand = 4

                },
                new Categories
                {
                    vi_name = "Quần Jean Lửng",
                    active = true,
                    order = 13,
                    id = 13,
                    id_brand = 5

                },
                new Categories
                {
                    vi_name = "Quần Jean Sexy",
                    active = true,
                    order = 14,
                    id = 14,
                    id_brand = 5

                },
                new Categories
                {
                    vi_name = "Quần Jean Hở Hang",
                    active = true,
                    order = 15,
                    id = 15,
                    id_brand = 5

                },
                new Categories
                {
                    vi_name = "Nón Lá làm từ lá",
                    active = true,
                    order = 16,
                    id = 16,
                    id_brand = 6

                },
                new Categories
                {
                    vi_name = "Nón Lá Thép",
                    active = true,
                    order = 17,
                    id = 17,
                    id_brand = 6

                },
                new Categories
                {
                    vi_name = "Nón Lá BêTong",
                    active = true,
                    order = 18,
                    id = 18,
                    id_brand = 6

                },
                new Categories
                {
                    vi_name = "Citi W1",
                    active = true,
                    order = 19,
                    id = 19,
                    id_brand = 7

                },
                new Categories
                {
                    vi_name = "Citi W2",
                    active = true,
                    order = 20,
                    id = 20,
                    id_brand = 7

                },
                new Categories
                {
                    vi_name = "Citi W3",
                    active = true,
                    order = 21,
                    id = 21,
                    id_brand = 7

                },
                new Categories
                {
                    vi_name = "Ome A",
                    active = true,
                    order = 22,
                    id = 22,
                    id_brand = 8

                },
                new Categories
                {
                    vi_name = "Ome B",
                    active = true,
                    order = 23,
                    id = 23,
                    id_brand = 8

                },
                new Categories
                {
                    vi_name = "Ome C",
                    active = true,
                    order = 24,
                    id = 24,
                    id_brand = 8

                },
                new Categories
                {
                    vi_name = "Rolex I",
                    active = true,
                    order = 25,
                    id = 25,
                    id_brand = 9

                },
                new Categories
                {
                    vi_name = "Rolex II",
                    active = true,
                    order = 26,
                    id = 26,
                    id_brand = 9

                },
                new Categories
                {
                    vi_name = "Rolex III",
                    active = true,
                    order = 27,
                    id = 27,
                    id_brand = 9

                }
            );
            builder.Entity<Items>().HasData(
                new Items
                {
                    id = 1,
                    name = "Item 1",
                    vi_info = "Thông Tin của item 1 sẽ hiện ở đây",
                    en_info = "There are some infos about Item 1",
                    img = "item1.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 2,
                    name = "Item 2",
                    vi_info = "Thông Tin của item 2 sẽ hiện ở đây",
                    en_info = "There are some infos about Item 2",
                    img = "item2.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 3,
                    name = "Item 3",
                    vi_info = "Thông Tin của item 3 sẽ hiện ở đây",
                    en_info = "There are some infos about Item 3",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 4,
                    name = "Item I",
                    vi_info = "Thông Tin của item I sẽ hiện ở đây",
                    en_info = "There are some infos about Item I",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 5,
                    name = "Item II",
                    vi_info = "Thông Tin của item II sẽ hiện ở đây",
                    en_info = "There are some infos about Item II",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 6,
                    name = "Item III",
                    vi_info = "Thông Tin của item III sẽ hiện ở đây",
                    en_info = "There are some infos about Item III",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 7,
                    name = "Item X",
                    vi_info = "Thông Tin của item X sẽ hiện ở đây",
                    en_info = "There are some infos about Item X",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 8,
                    name = "Item X1",
                    vi_info = "Thông Tin của item X1 sẽ hiện ở đây",
                    en_info = "There are some infos about Item X1",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 8,
                    name = "Item X12",
                    vi_info = "Thông Tin của item X12 sẽ hiện ở đây",
                    en_info = "There are some infos about Item X12",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 9,
                    name = "Item X121",
                    vi_info = "Thông Tin của item X121 sẽ hiện ở đây",
                    en_info = "There are some infos about Item X121",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 10,
                    name = "Item XEEEEEEE",
                    vi_info = "Thông Tin của item XEEêEEêEE sẽ hiện ở đây",
                    en_info = "There are some infos about Item XEEeEEEEEEEeeeE",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 1

                },
                new Items
                {
                    id = 11,
                    name = "Item XEEEEEEE",
                    vi_info = "Thông Tin của item XEEêEEêEE sẽ hiện ở đây",
                    en_info = "There are some infos about Item XEEeEEEEEEEeeeE",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 2

                },
                new Items
                {
                    id = 12,
                    name = "Item XEEEEádasdasfsafasE ",
                    vi_info = "Thông Tin của item XEEEEádasdasfsafasE sẽ hiện ở đây",
                    en_info = "There are some infos about Item XEEEEádasdasfsafasE",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 2

                },
                new Items
                {
                    id = 13,
                    name = "Item qưtegdsdgegwggegw ",
                    vi_info = "Thông Tin của item qưtegdsdgegwggegw sẽ hiện ở đây",
                    en_info = "There are some infos about Item qưtegdsdgegwggegw",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 2

                },
                new Items
                {
                    id = 14,
                    name = "fasfasfasfsafsafsaf ",
                    vi_info = "Thông Tin của item fasfasfasfsafsafsaf sẽ hiện ở đây",
                    en_info = "There are some infos about Item fasfasfasfsafsafsaf",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 2

                },
                new Items
                {
                    id = 15,
                    name = "fasfasfasfsafsafsaf ",
                    vi_info = "Thông Tin của item fasfasfasfsafsafsaf sẽ hiện ở đây",
                    en_info = "There are some infos about Item fasfasfasfsafsafsaf",
                    img = "item3.pnj",
                    volatility = 0,
                    trade_max = 200,
                    trade_min = 100,
                    id_admin = 1,
                    id_category = 2

                }
            );


        }

    }
}
