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
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Shippings> Shippings { get; set; }
        public DbSet<Citys> Citys { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }

        public DbSet<ServiceFeeDetails> ServiceFeeDetails { get; set; }


        //Server Name - Escanor: DESKTOP-9LUKN91\SQLEXPRESS
        //Server Name - Khoa: DESKTOP-EH88R88\SQLEXPRESS
        //Server Name - Kiet: DESKTOP-9IBF91T\SQLEXPRESS
        // genos - C9UR2EM
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
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    email = "genos@gmail.com",
                    level = 1,
                    date_create = DateTime.Parse("1969-04-05"),
                    active = true,
                },
                new Admins
                {
                    id = 2,
                    name = "duytran",
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    email = "duytran@gmail.com",
                    level = 1,
                    date_create = DateTime.Parse("2019-01-05"),
                    active = true,
                },
                new Admins
                {
                    id = 3,
                    name = "khoado",
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    email = "khoado@gmail.com",
                    level = 1,
                    date_create = DateTime.Parse("2019-01-05"),
                    active = true,
                },
                new Admins
                {
                    id = 4,
                    name = "kietnguyen",
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    email = "kietnguyen161@gmail.com",
                    level = 1,
                    date_create = DateTime.Parse("2019-01-05"),
                    active = true,
                },
                new Admins
                {
                    id = 5,
                    name = "kietnguyen1",
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    email = "kietnguyen1612@gmail.com",
                    level = 2,
                    date_create = DateTime.Parse("2019-01-05"),
                    active = true,
                },
                new Admins
                {
                    id = 6,
                    name = "kietnguyen2",
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    email = "kietnguyen1612@gmail.com",
                    level = 3,
                    date_create = DateTime.Parse("2019-01-05"),
                    active = true,
                },
                new Admins
                {
                    id = 7,
                    name = "kietnguyen3",
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    email = "kietnguyen1617@gmail.com",
                    level = 3,
                    date_create = DateTime.Parse("2019-01-05"),
                    active = true,
                },
                new Admins
                {
                    id = 8,
                    name = "kietnguyen4",
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    email = "kietnguyen1614@gmail.com",
                    level = 4,
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
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    level_seller = 1,
                    score_buyer = 1,
                    date_registion = DateTime.Parse("2019-01-05"),
                    active = true,
                    phone = "0911111111",
                    address = "bitexco quan 1",
                    id_city = 1
                },
                new Users
                {
                    id = 2,
                    name = "Lê văn tý",
                    email = "ty@gmail.com",
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    level_seller = 2,
                    score_buyer = 2,
                    date_registion = DateTime.Parse("2019-01-05"),
                    active = true,
                    phone = "091222222",
                    address = "land max",
                    id_city = 1
                },
                new Users
                {
                    id = 3,
                    name = "Lê văn tun",
                    email = "tun@gmail.com",
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    level_seller = 3,
                    score_buyer = 3,
                    date_registion = DateTime.Parse("2019-01-05"),
                    active = true,
                    phone = "091111845891",
                    address = "daslat quan 1",
                    id_city = 3
                },
                new Users
                {
                    id = 4,
                    name = "Lê văn tet",
                    email = "tet@gmail.com",
                    password = "9ce749361e3b28e16df8b7adefdb2a4e",
                    level_seller = 4,
                    score_buyer = 4,
                    date_registion = DateTime.Parse("2019-01-05"),
                    active = true,
                    phone = "0914135111",
                    address = "lamdong quan 1",
                    id_city = 2
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
                    name = "Adidas",
                    active = true,
                    order = 1,
                    id_department = 1,
                    img = "adidas.jpg"
                },
                new Brands
                {
                    id = 2,
                    name = "Nike",
                    active = true,
                    order = 2,
                    id_department = 1,
                    img = "nike.jpg"
                },
                new Brands
                {
                    id = 3,
                    name = "Bittis",
                    active = true,
                    order = 3,
                    id_department = 1,
                    img = "bitis.jpg"
                },
                new Brands
                {
                    id = 4,
                    name = "An Phước",
                    active = true,
                    order = 4,
                    id_department = 2,
                    img = "anphuoc.jpg"
                },
                new Brands
                {

                    name = "Zara",
                    active = true,
                    order = 5,
                    id = 5,
                    id_department = 2,
                    img = "zara.jpg"
                },
                new Brands
                {
                    name = "Burberry",
                    active = true,
                    order = 6,
                    id = 6,
                    id_department = 2,
                    img = "burberry.jpg"
                },
                new Brands
                {
                    name = "Citizen",
                    active = true,
                    order = 7,
                    id = 7,
                    id_department = 3,
                    img = "citizen.jpg"
                },
                new Brands
                {
                    name = "Omega",
                    active = true,
                    order = 8,
                    id = 8,
                    id_department = 3,
                    img = "omega.jpg"
                },
                new Brands
                {
                    name = "Rolex",
                    active = true,
                    order = 9,
                    id = 9,
                    id_department = 3,
                    img = "rolex.jpg"
                }
            );
            builder.Entity<Categories>().HasData(
                new Categories
                {
                    name = "Yezzze",
                    active = true,
                    order = 1,
                    id = 1,
                    id_brand = 1

                },
                new Categories
                {
                    name = "Ultraboost",
                    active = true,
                    order = 2,
                    id = 2,
                    id_brand = 1

                },
                new Categories
                {
                    name = "NMD",
                    active = true,
                    order = 3,
                    id = 3,
                    id_brand = 1

                },
                new Categories
                {
                    name = "AirForce",
                    active = true,
                    order = 4,
                    id = 4,
                    id_brand = 2

                },
                new Categories
                {
                    name = "AirMax",
                    active = true,
                    order = 5,
                    id = 5,
                    id_brand = 2

                },
                new Categories
                {
                    name = "Jordan",
                    active = true,
                    order = 6,
                    id = 6,
                    id_brand = 2

                },
                new Categories
                {
                    name = "Hunter",
                    active = true,
                    order = 7,
                    id = 7,
                    id_brand = 3

                },
                new Categories
                {
                    name = "Hunter x Hunter",
                    active = true,
                    order = 8,
                    id = 8,
                    id_brand = 3

                },
                new Categories
                {
                    name = "Nameless",
                    active = true,
                    order = 9,
                    id = 9,
                    id_brand = 3

                },
                new Categories
                {
                    name = "Áo Sơ Mi",
                    active = true,
                    order = 10,
                    id = 10,
                    id_brand = 4

                },
                new Categories
                {
                    name = "Veston",
                    active = true,
                    order = 11,
                    id = 11,
                    id_brand = 4

                },
                new Categories
                {
                    name = "Áo Thun",
                    active = true,
                    order = 12,
                    id = 12,
                    id_brand = 4

                },
                new Categories
                {
                    name = "Áo Phong",
                    active = true,
                    order = 13,
                    id = 13,
                    id_brand = 5

                },
                new Categories
                {
                    name = "Áo Blazer",
                    active = true,
                    order = 14,
                    id = 14,
                    id_brand = 5

                },
                new Categories
                {
                    name = "Áo Khoác",
                    active = true,
                    order = 15,
                    id = 15,
                    id_brand = 5

                },
                new Categories
                {
                    name = "Beige Classic",
                    active = true,
                    order = 16,
                    id = 16,
                    id_brand = 6

                },
                new Categories
                {
                    name = "Navy",
                    active = true,
                    order = 17,
                    id = 17,
                    id_brand = 6

                },
                new Categories
                {
                    name = "Indigo Denim",
                    active = true,
                    order = 18,
                    id = 18,
                    id_brand = 6

                },
                new Categories
                {
                    name = "Corso",
                    active = true,
                    order = 19,
                    id = 19,
                    id_brand = 7

                },
                new Categories
                {
                    name = "Avion",
                    active = true,
                    order = 20,
                    id = 20,
                    id_brand = 7

                },
                new Categories
                {
                    name = "Brycen",
                    active = true,
                    order = 21,
                    id = 21,
                    id_brand = 7

                },
                new Categories
                {
                    name = "Seamaster",
                    active = true,
                    order = 22,
                    id = 22,
                    id_brand = 8

                },
                new Categories
                {
                    name = "Speedmaster",
                    active = true,
                    order = 23,
                    id = 23,
                    id_brand = 8

                },
                new Categories
                {
                    name = "Constellation",
                    active = true,
                    order = 24,
                    id = 24,
                    id_brand = 8

                },
                new Categories
                {
                    name = "SKY-DWELLER",
                    active = true,
                    order = 25,
                    id = 25,
                    id_brand = 9

                },
                new Categories
                {
                    name = "AIR-KING",
                    active = true,
                    order = 26,
                    id = 26,
                    id_brand = 9

                },
                new Categories
                {
                    name = "EXPLORER II",
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
                    name = "YEEZY BOOST 350 V2 ",
                    vi_info = "<h2>#01. Yeezy 350 V2 Static</h2>< p >< img alt = 'Giày Yeezy Boost 350 V2 Static' src = 'https://a.ipricegroup.com/media/Sneaker/Yeezy/Giay_Yeezy_Boost_350_V2_Static.jpg' title = 'Giày Yeezy Boost 350 V2 Static' /></ p >< p > &nbsp;</ p > < p > Yeezy 350 V2 Static th & uacute; vị ở chỗ, n & oacute; được l&agrave; m ra d & agrave; nh cho những người đ & atilde; sở hữu đ & ocirc; i OG Creams v&agrave; Wave Runners. C & aacute; c chi tiết trắng đen được kết hợp một c&aacute; ch s&aacute; ng tạo tr & ecirc; n nền vải Primeknit v & agrave; cảm gi&aacute; c thoải m & aacute; i khi mang tr&ecirc; n d&ograve; ng 350 V2 như mọi khi. Giới sưu tầm gi&agrave; y sneaker đang v&ocirc; c & ugrave; ng tr&ocirc; ng chờ ng & agrave; y ra mắt dự đo & aacute; n v&agrave; o cuối 2018 n & agrave; y.</ p > ",
                    en_info = "<h2>#01. Yeezy 350 V2 Static</h2>< p >< img alt = 'Giày Yeezy Boost 350 V2 Static' src = 'https://a.ipricegroup.com/media/Sneaker/Yeezy/Giay_Yeezy_Boost_350_V2_Static.jpg' title = 'Giày Yeezy Boost 350 V2 Static' /></ p >< p > &nbsp;</ p > < p > Yeezy 350 V2 Static th & uacute; vị ở chỗ, n & oacute; được l&agrave; m ra d & agrave; nh cho những người đ & atilde; sở hữu đ & ocirc; i OG Creams v&agrave; Wave Runners. C & aacute; c chi tiết trắng đen được kết hợp một c&aacute; ch s&aacute; ng tạo tr & ecirc; n nền vải Primeknit v & agrave; cảm gi&aacute; c thoải m & aacute; i khi mang tr&ecirc; n d&ograve; ng 350 V2 như mọi khi. Giới sưu tầm gi&agrave; y sneaker đang v&ocirc; c & ugrave; ng tr&ocirc; ng chờ ng & agrave; y ra mắt dự đo & aacute; n v&agrave; o cuối 2018 n & agrave; y.</ p > ",
                    img = "yeezy.jpg",
                    volatility = 0,
                    trade_max = 3500000,
                    trade_min = 3000000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,
                    lowest_ask = 3400000,
                    highest_bid = 2900000
                },
                new Items
                {
                    id = 2,
                    name = "Yeezy Boost 700 V2",
                    vi_info = "Thực hiện một tuyên bố với adidas Yeezy Boost 700 V2 Vanta trên đôi chân của bạn. Yeezy Boost 700 V2 này đi kèm với đế trên màu đen, đế giữa màu đen và đế màu đen. Những đôi giày thể thao này được phát hành vào tháng 6 năm 2019 và được bán lẻ với giá 300 đô la.",
                    en_info = "Make a statement with the adidas Yeezy Boost 700 V2 Vanta on your feet. This Yeezy Boost 700 V2 comes with a black upper, black midsole, and a black sole. These sneakers released in June 2019 and retailed for $300. Provide the world with some Yeezy vibes after grabbing these on StockX.",
                    img = "yeezy2.jpg",
                    volatility = 0,
                    trade_max = 5000000,
                    trade_min = 4000000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,
                    lowest_ask = 4900000,
                    highest_bid = 3900000
                },
                new Items
                {
                    id = 3,
                    name = "Yeezy 500 Super Moon",
                    vi_info = "Những chiếc Yeezy 500 Super Moon Yellows mới này đang nóng hơn cả một ngọn lửa Jackson Hole. Colorway mới nhất trong dòng Yeezy 500, chúng sử dụng đệm adiPrene thay cho BOOST và có vẻ ngoài to hơn sẽ phát triển trên bạn (tin tưởng chúng tôi). Cặp màu vàng toàn mặt trăng này là colorway đầu tiên bị rơi, như là một phần của gói Yeezy season 6, tiếp theo là bản phát hành rộng hơn vào tháng 6 năm 2018. Người hâm mộ của Ye cần phải thực hiện 'No Mistakes' và thêm colorway Yeezy 500 này vào bộ sưu tập càng sớm càng tốt.",
                    en_info = "These new Yeezy 500 Super Moon Yellows are coming in hotter than a Jackson Hole bonfire. The latest colorway in the Yeezy 500 line, these use adiPrene cushioning in place of BOOST and feature a chunkier look that will grow on you (trust us). This all-moon yellow pair was the first colorway that dropped, as part of the Yeezy season 6 bundle, followed by a wider release in June of 2018. Fans of Ye need to make 'No Mistakes' and add this Yeezy 500 colorway into the collection asap.",
                    img = "yeezy3.jpg",
                    volatility = 0,
                    trade_max = 6000000,
                    trade_min = 5000000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,
                    lowest_ask = 5900000,
                    highest_bid = 4950000
                },
                new Items
                {
                    id = 4,
                    name = "Yeezy Powerphase Calabasas Core Black",
                    vi_info = "Colorway thứ ba của dòng, adidas Yeezy Powerphase Calabasas này có bảng màu đen đơn sắc.Ngoài ra còn có nhãn hiệu Adidas màu xanh lá cây bên cạnh logo Trefoil màu đỏ.Colorway cổ điển này được phát hành vào tháng 3 năm 2017 với giá $ 120.",
                    en_info = "The third colorway of the series, this adidas Yeezy Powerphase Calabasas features a monochromatic core black color palette. There is also Adidas branding in green next to a red Trefoil logo. This classic colorway released in March 2017 for $120.",
                    img = "yeezy4.jpg",
                    volatility = 0,
                    trade_max = 7000000,
                    trade_min = 5500000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,


                },
                new Items
                {
                    id = 5,
                    name = "Yeezy Boost 750 Light Grey Glow In the Dark",
                    vi_info = "Giày adidas hợp tác đầu tiên của Kanye West, Yeezy Boost 750 OG, được phát hành trên toàn thế giới vào tháng 2 năm 2017 với số lượng cực kỳ hạn chế. Các thiết kế giống như giày cao chót vót thể hiện sự khác biệt hoàn toàn so với các thiết kế trước đó của West, với phần trên bằng da lộn màu nâu nhạt được gắn trên một khung sườn, dụng cụ cao su được trang bị Boost trong một bóng râm trắng tinh tế. Thiết kế tối giản được tạo điểm nhấn với dây buộc quá khổ, chi tiết đục lỗ trên hộp ngón chân, và một dây đeo chân giữa rộng trong một chế tạo twill tonal. Một dây kéo ẩn ở phía bên cung cấp dễ dàng bật và tắt.",
                    en_info = "Kanye West’s very first collaborative adidas shoe, the Yeezy Boost 750 ‘OG’ released worldwide February 2017 in extremely limited quantities. The towering boot-like builds showcases a radical departure from West’s previous designs, featuring a light brown all-suede upper mounted atop a partially ribbed, Boost-equipped rubber tooling in a subtle off-white shade. The minimalist design is accented with oversized rope laces, perforated detailing on the toe box, and a wide midfoot strap in a tonal twill fabrication. A hidden zipper on the lateral side provides easy on and off.",
                    img = "yeezy5.jpg",
                    volatility = 0,
                    trade_max = 4500000,
                    trade_min = 4000000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,
                    lowest_ask = 4400000,
                    highest_bid = 3900000

                },
                new Items
                {
                    id = 6,
                    name = "Yeezy Desert Boot Oil",
                    vi_info = "Khen ngợi Coachella phù hợp với adidas Yeezy Desert Boot Oil. Boot Yeezy này đi kèm với đế trên, đế dầu và đế dầu. Những đôi giày thể thao này được phát hành vào tháng 4 năm 2019 và bán lẻ với giá 200 đô la. Mua một số khởi động mới cho Dịch vụ Chủ nhật, ngay tại đây trên StockX.",
                    en_info = "Compliment the Coachella fit with the adidas Yeezy Desert Boot Oil. This Yeezy Boot comes with an oil upper, oil midsole, and an oil sole. These sneakers released in April 2019 and retailed for $200. Buy some fresh boots for Sunday Service, right here on StockX.",
                    img = "yeezy6.jpg",
                    volatility = 0,
                    trade_max = 8000000,
                    trade_min = 6000000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,


                },
                new Items
                {
                    id = 7,
                    name = "Yeezy Boost 950 Pirate Black",
                    vi_info = "Khen ngợi Coachella phù hợp với adidas Yeezy Desert Boot Oil. Boot Yeezy này đi kèm với đế trên, đế dầu và đế dầu. Những đôi giày thể thao này được phát hành vào tháng 4 năm 2019 và bán lẻ với giá 200 đô la. Mua một số khởi động mới cho Dịch vụ Chủ nhật, ngay tại đây trên StockX.",
                    en_info = "Compliment the Coachella fit with the adidas Yeezy Desert Boot Oil. This Yeezy Boot comes with an oil upper, oil midsole, and an oil sole. These sneakers released in April 2019 and retailed for $200. Buy some fresh boots for Sunday Service, right here on StockX.",
                    img = "yeezy7.jpg",
                    volatility = 0,
                    trade_max = 8000000,
                    trade_min = 6000000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,
                    lowest_ask = 7800000,
                    highest_bid = 6350000

                },
                new Items
                {
                    id = 16,
                    name = "Yeezy 500 Soft Vision",
                    vi_info = "Khen ngợi Coachella phù hợp với adidas Yeezy Desert Boot Oil. Boot Yeezy này đi kèm với đế trên, đế dầu và đế dầu. Những đôi giày thể thao này được phát hành vào tháng 4 năm 2019 và bán lẻ với giá 200 đô la. Mua một số khởi động mới cho Dịch vụ Chủ nhật, ngay tại đây trên StockX.",
                    en_info = "Compliment the Coachella fit with the adidas Yeezy Desert Boot Oil. This Yeezy Boot comes with an oil upper, oil midsole, and an oil sole. These sneakers released in April 2019 and retailed for $200. Buy some fresh boots for Sunday Service, right here on StockX.",
                    img = "yeezy16.jpg",
                    volatility = 0,
                    trade_max = 8300000,
                    trade_min = 6600000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,


                },
                new Items
                {
                    id = 8,
                    name = "Yeezy Classic",
                    vi_info = "Khen ngợi Coachella phù hợp với adidas Yeezy Desert Boot Oil. Boot Yeezy này đi kèm với đế trên, đế dầu và đế dầu. Những đôi giày thể thao này được phát hành vào tháng 4 năm 2019 và bán lẻ với giá 200 đô la. Mua một số khởi động mới cho Dịch vụ Chủ nhật, ngay tại đây trên StockX.",
                    en_info = "Compliment the Coachella fit with the adidas Yeezy Desert Boot Oil. This Yeezy Boot comes with an oil upper, oil midsole, and an oil sole. These sneakers released in April 2019 and retailed for $200. Buy some fresh boots for Sunday Service, right here on StockX.",
                    img = "yeezy8.jpg",
                    volatility = 0,
                    trade_max = 1000000,
                    trade_min = 600000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,

                },
                new Items
                {
                    id = 9,
                    name = "Yeezy Boost 500 Bone White",
                    vi_info = "Khen ngợi Coachella phù hợp với adidas Yeezy Desert Boot Oil. Boot Yeezy này đi kèm với đế trên, đế dầu và đế dầu. Những đôi giày thể thao này được phát hành vào tháng 4 năm 2019 và bán lẻ với giá 200 đô la. Mua một số khởi động mới cho Dịch vụ Chủ nhật, ngay tại đây trên StockX.",
                    en_info = "Compliment the Coachella fit with the adidas Yeezy Desert Boot Oil. This Yeezy Boot comes with an oil upper, oil midsole, and an oil sole. These sneakers released in April 2019 and retailed for $200. Buy some fresh boots for Sunday Service, right here on StockX.",
                    img = "yeezy9.jpg",
                    volatility = 0,
                    trade_max = 11000000,
                    trade_min = 6000000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,
                    lowest_ask = 9900000,
                    highest_bid = 7050000

                },
                new Items
                {
                    id = 10,
                    name = "Yeezy Boost 500 Bone White V2",
                    vi_info = "Khen ngợi Coachella phù hợp với adidas Yeezy Desert Boot Oil. Boot Yeezy này đi kèm với đế trên, đế dầu và đế dầu. Những đôi giày thể thao này được phát hành vào tháng 4 năm 2019 và bán lẻ với giá 200 đô la. Mua một số khởi động mới cho Dịch vụ Chủ nhật, ngay tại đây trên StockX.",
                    en_info = "Compliment the Coachella fit with the adidas Yeezy Desert Boot Oil. This Yeezy Boot comes with an oil upper, oil midsole, and an oil sole. These sneakers released in April 2019 and retailed for $200. Buy some fresh boots for Sunday Service, right here on StockX.",
                    img = "yeezy9.jpg",
                    volatility = 0,
                    trade_max = 11000000,
                    trade_min = 6000000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,
                    lowest_ask = 10500000,
                    highest_bid = 7650000

                },
                new Items
                {
                    id = 11,
                    name = "Yeezy Boost 500 Bone White V3",
                    vi_info = "Khen ngợi Coachella phù hợp với adidas Yeezy Desert Boot Oil. Boot Yeezy này đi kèm với đế trên, đế dầu và đế dầu. Những đôi giày thể thao này được phát hành vào tháng 4 năm 2019 và bán lẻ với giá 200 đô la. Mua một số khởi động mới cho Dịch vụ Chủ nhật, ngay tại đây trên StockX.",
                    en_info = "Compliment the Coachella fit with the adidas Yeezy Desert Boot Oil. This Yeezy Boot comes with an oil upper, oil midsole, and an oil sole. These sneakers released in April 2019 and retailed for $200. Buy some fresh boots for Sunday Service, right here on StockX.",
                    img = "yeezy9.jpg",
                    volatility = 0,
                    trade_max = 11000000,
                    trade_min = 6000000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,
                    lowest_ask = 8500000,
                    highest_bid = 6150000

                },
                new Items
                {
                    id = 12,
                    name = "Yeezy 500 Soft Vision V2",
                    vi_info = "Khen ngợi Coachella phù hợp với adidas Yeezy Desert Boot Oil. Boot Yeezy này đi kèm với đế trên, đế dầu và đế dầu. Những đôi giày thể thao này được phát hành vào tháng 4 năm 2019 và bán lẻ với giá 200 đô la. Mua một số khởi động mới cho Dịch vụ Chủ nhật, ngay tại đây trên StockX.",
                    en_info = "Compliment the Coachella fit with the adidas Yeezy Desert Boot Oil. This Yeezy Boot comes with an oil upper, oil midsole, and an oil sole. These sneakers released in April 2019 and retailed for $200. Buy some fresh boots for Sunday Service, right here on StockX.",
                    img = "yeezy16.jpg",
                    volatility = 0,
                    trade_max = 8300000,
                    trade_min = 6600000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,
                    lowest_ask = 8490000,
                    highest_bid = 4350000

                },
                new Items
                {
                    id = 13,
                    name = "Yeezy 500 Soft Vision V4",
                    vi_info = "Khen ngợi Coachella phù hợp với adidas Yeezy Desert Boot Oil. Boot Yeezy này đi kèm với đế trên, đế dầu và đế dầu. Những đôi giày thể thao này được phát hành vào tháng 4 năm 2019 và bán lẻ với giá 200 đô la. Mua một số khởi động mới cho Dịch vụ Chủ nhật, ngay tại đây trên StockX.",
                    en_info = "Compliment the Coachella fit with the adidas Yeezy Desert Boot Oil. This Yeezy Boot comes with an oil upper, oil midsole, and an oil sole. These sneakers released in April 2019 and retailed for $200. Buy some fresh boots for Sunday Service, right here on StockX.",
                    img = "yeezy16.jpg",
                    volatility = 0,
                    trade_max = 8300000,
                    trade_min = 6600000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,
                    lowest_ask = 6200000,
                    highest_bid = 5350000

                },
                new Items
                {
                    id = 14,
                    name = "Yeezy Powerphase Calabasas Core Black v2",
                    vi_info = "Colorway thứ ba của dòng, adidas Yeezy Powerphase Calabasas này có bảng màu đen đơn sắc.Ngoài ra còn có nhãn hiệu Adidas màu xanh lá cây bên cạnh logo Trefoil màu đỏ.Colorway cổ điển này được phát hành vào tháng 3 năm 2017 với giá $ 120.",
                    en_info = "The third colorway of the series, this adidas Yeezy Powerphase Calabasas features a monochromatic core black color palette. There is also Adidas branding in green next to a red Trefoil logo. This classic colorway released in March 2017 for $120.",
                    img = "yeezy4.jpg",
                    volatility = 0,
                    trade_max = 7000000,
                    trade_min = 5500000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,


                },
                new Items
                {
                    id = 15,
                    name = "Yeezy Powerphase Calabasas Core Black v3",
                    vi_info = "Colorway thứ ba của dòng, adidas Yeezy Powerphase Calabasas này có bảng màu đen đơn sắc.Ngoài ra còn có nhãn hiệu Adidas màu xanh lá cây bên cạnh logo Trefoil màu đỏ.Colorway cổ điển này được phát hành vào tháng 3 năm 2017 với giá $ 120.",
                    en_info = "The third colorway of the series, this adidas Yeezy Powerphase Calabasas features a monochromatic core black color palette. There is also Adidas branding in green next to a red Trefoil logo. This classic colorway released in March 2017 for $120.",
                    img = "yeezy4.jpg",
                    volatility = 0,
                    trade_max = 7000000,
                    trade_min = 5500000,
                    id_admin = 1,
                    id_category = 1,
                    active = true,


                }
            );
            builder.Entity<ds_Size>().HasData(
                new ds_Size
                {
                    id = 1,
                    VN = 30,
                    US = 25,
                    UK = 20,
                    Inch = 13,
                    Centimet = 45,
                    id_Depart = 1
                },
                new ds_Size
                {
                    id = 2,
                    VN = 35,
                    US = 30,
                    UK = 25,
                    Inch = 18,
                    Centimet = 50,
                    id_Depart = 1
                },
                new ds_Size
                {
                    id = 3,
                    VN = 40,
                    US = 35,
                    UK = 30,
                    Inch = 21,
                    Centimet = 55,
                    id_Depart = 1
                },
                new ds_Size
                {
                    id = 4,
                    VN = 35,
                    US = 35,
                    UK = 35,
                    Inch = 21,
                    Centimet = 55,
                    id_Depart = 2
                },
                new ds_Size
                {
                    id = 5,
                    VN = 36,
                    US = 36,
                    UK = 36,
                    Inch = 25,
                    Centimet = 60,
                    id_Depart = 2
                },
                new ds_Size
                {
                    id = 6,
                    VN = 37,
                    US = 37,
                    UK = 37,
                    Inch = 28,
                    Centimet = 65,
                    id_Depart = 2
                },
                new ds_Size
                {
                    id = 7,
                    VN = 44,
                    US = 44,
                    UK = 44,
                    Inch = 21,
                    Centimet = 55,
                    id_Depart = 3
                },
                new ds_Size
                {
                    id = 8,
                    VN = 39,
                    US = 39,
                    UK = 39,
                    Inch = 18,
                    Centimet = 40,
                    id_Depart = 3
                }


            );
            builder.Entity<Sizes>().HasData(
                new Sizes
                {
                    id = 1,
                    lowest_ask = 3400000,
                    highest_bid = 2900000,
                    last_sale = 3000000,
                    id_ds_size = 1,
                    id_item = 1
                },
                new Sizes
                {
                    id = 2,
                    lowest_ask = 3500000,
                    highest_bid = 2800000,
                    last_sale = 3000000,
                    id_ds_size = 2,
                    id_item = 1
                },
                new Sizes
                {
                    id = 3,
                    lowest_ask = 4000000,
                    highest_bid = 1000000,
                    last_sale = 2900000,
                    id_ds_size = 3,
                    id_item = 1
                },
                new Sizes
                {
                    id = 4,
                    lowest_ask = 4900000,
                    highest_bid = 3900000,
                    last_sale = 4000000,
                    id_ds_size = 2,
                    id_item = 2
                },
                new Sizes
                {
                    id = 5,
                    lowest_ask = 6000000,
                    highest_bid = 2550000,
                    last_sale = 4500000,
                    id_ds_size = 1,
                    id_item = 2
                },
                new Sizes
                {
                    id = 6,
                    lowest_ask = 5900000,
                    highest_bid = 4950000,
                    last_sale = 5000000,
                    id_ds_size = 2,
                    id_item = 3
                },
                new Sizes
                {
                    id = 7,
                    lowest_ask = 6000000,
                    highest_bid = 3600000,
                    last_sale = 3220000,
                    id_ds_size = 1,
                    id_item = 3
                },
                new Sizes
                {
                    id = 8,
                    lowest_ask = 4400000,
                    highest_bid = 3900000,
                    last_sale = 3220000,
                    id_ds_size = 2,
                    id_item = 5
                },
                new Sizes
                {
                    id = 9,
                    lowest_ask = 5300000,
                    highest_bid = 2250000,
                    last_sale = 2220000,
                    id_ds_size = 3,
                    id_item = 5
                },
                //lam post mau toi size 9
                new Sizes
                {
                    id = 10,
                    lowest_ask = 7800000,
                    highest_bid = 6150000,
                    last_sale = 5220000,
                    id_ds_size = 2,
                    id_item = 7
                },
                new Sizes
                {
                    id = 11,
                    lowest_ask = 8300000,
                    highest_bid = 6350000,
                    last_sale = 7220000,
                    id_ds_size = 1,
                    id_item = 7
                },
                new Sizes
                {
                    id = 12,
                    lowest_ask = 9900000,
                    highest_bid = 7050000,
                    last_sale = 8020000,
                    id_ds_size = 2,
                    id_item = 9
                },
                new Sizes
                {
                    id = 13,
                    lowest_ask = 10500000,
                    highest_bid = 7650000,
                    last_sale = 8220000,
                    id_ds_size = 2,
                    id_item = 10
                },
                new Sizes
                {
                    id = 14,
                    lowest_ask = 8500000,
                    highest_bid = 6150000,
                    last_sale = 7020000,
                    id_ds_size = 1,
                    id_item = 11
                },
                new Sizes
                {
                    id = 15,
                    lowest_ask = 8490000,
                    highest_bid = 4350000,
                    last_sale = 6220000,
                    id_ds_size = 3,
                    id_item = 12
                },
                new Sizes
                {
                    id = 16,
                    lowest_ask = 6200000,
                    highest_bid = 5350000,
                    last_sale = 220000,
                    id_ds_size = 1,
                    id_item = 13
                }

            );
            builder.Entity<News>().HasData(
                new News
                {
                    id = 1,
                    vi_title = "Thời trang việt nam được cách mạng sau MV 'Hãy Trao Cho Anh - Sơn Tùng MT-P'",
                    en_title = "News about shoes",
                    vi_content = "<h2>Anh Tiến Anh Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA khẳng định: &ldquo;Đảm bảo chỉ chưa đầy nửa năm, s&ocirc;ng T&ocirc; Lịch sẽ hồi sinh ho&agrave;n to&agrave;n&rdquo;.</h2><p>Sự kiện:&nbsp;</p><h3><a href=‘https://www.24h.com.vn/xu-ly-o-nhiem-song-to-lich-c46e5865.html’ title=‘Xử lý ô nhiễm sông Tô Lịch’>Xử l&yacute; &ocirc; nhiễm s&ocirc;ng T&ocirc; Lịch</a></h3><p>Đ&oacute; l&agrave; kỹ sư, doanh nh&acirc;n Vũ Tiến Anh, Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA &ndash; với c&ocirc;ng nghệ xử l&yacute; MET (Mechanical Energy Technologies &ndash; C&ocirc;ng nghệ Năng lượng cơ học).</p><p><strong>Chờ đợi 3 năm để được cấp bằng s&aacute;ng chế</strong></p><p>Anh Vũ Tiến Anh hẹn gặp ph&oacute;ng vi&ecirc;n Infonet tại văn ph&ograve;ng C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA (C&ocirc;ng ty TA) v&agrave; cũng l&agrave; nh&agrave; ri&ecirc;ng, trong một con ng&otilde; nhỏ tr&ecirc;n phố T&ocirc;n Đức Thắng, Đống Đa, H&agrave; Nội.</p><p>Tại đ&acirc;y, vị Gi&aacute;m đốc sinh năm 1982 say sưa khoe với ph&oacute;ng vi&ecirc;n về việc c&ocirc;ng nghệ xử l&yacute; nước MET của c&ocirc;ng ty vừa ch&iacute;nh thức được Hội đồng khoa học của Bộ Khoa học v&agrave; C&ocirc;ng nghệ thẩm định, v&agrave; được Cục Sở hữu c&ocirc;ng nghiệp cấp bằng s&aacute;ng chế độc quyền.</p><p>Thay v&igrave; chỉ nhận lắp đặt c&aacute;c c&ocirc;ng tr&igrave;nh xử l&yacute; nước thải ở c&aacute;c doanh nghiệp tư nh&acirc;n v&agrave; c&aacute;c hộ d&acirc;n như trong thời gian qua,&nbsp;c&ocirc;ng ty đ&atilde; c&oacute; đủ tư c&aacute;ch để l&agrave;m c&aacute;c dự &aacute;n xử l&yacute; nước thải cấp bộ, cấp nh&agrave; nước v&agrave; cấp địa phương.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 1’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-1-1563329277-481-width660height495.jpg’ /></p><p>Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA Vũ Tiến Anh, người khởi nghiệp với hệ thống xử l&yacute; nước c&ocirc;ng nghệ MET.</p><p>Anh bảy tỏ, mong muốn trong tương lai gần c&oacute; thể triển khai m&ocirc; h&igrave;nh th&iacute; điểm xử l&yacute; nước đặt tại d&ograve;ng s&ocirc;ng T&ocirc; Lịch (H&agrave; Nội). Đ&acirc;y cũng l&agrave; mong muốn lớn nhất, đ&uacute;ng như ti&ecirc;u ch&iacute; của c&ocirc;ng ty l&agrave; &ldquo;hồi sinh những d&ograve;ng s&ocirc;ng bị &ocirc; nhiễm&rdquo;.</p><p>&quot;Ngay từ năm 2016 khi th&agrave;nh lập doanh nghiệp, t&ocirc;i đ&atilde; đặt m&aacute;y thử nghiệm tại s&ocirc;ng T&ocirc; Lịch, đoạn chảy qua cầu Mới &ndash; Nguyễn Tr&atilde;i. Kết quả cho thấy c&ocirc;ng nghệ xử l&yacute; MET tỏ ra hiệu quả r&otilde; rệt khi nước ban đầu nồng nặc m&ugrave;i h&ocirc;i thối đ&atilde; trở n&ecirc;n trong vắt&quot;.</p><p>&ldquo;Tuy nhi&ecirc;n, suốt từ đ&oacute; đến nay kh&ocirc;ng một ai ghi nhận th&agrave;nh quả của doanh nghiệp, đi đến đ&acirc;u người ta cũng d&egrave; bỉu, họ cho rằng hệ thống n&agrave;y qu&aacute; đơn giản, qu&aacute; th&ocirc; sơ.Đến b&acirc;y giờ, trong suy nghĩ của kh&ocirc;ng chỉ những người ngo&agrave;i ng&agrave;nh m&agrave; cả ở trong ng&agrave;nh, người ta vẫn cho rằng đ&atilde; xử l&yacute; nước thải l&agrave; phải c&oacute; h&oacute;a chất.Họ cho rằng kh&ocirc;ng thể xử l&yacute; được với c&ocirc;ng nghệ của ch&uacute;ng t&ocirc;i&rdquo;, anh Tiến Anh cho hay.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 2’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-2-1563329277-662-width660height495.jpg’ /></p><p>&Ocirc;ng Ousmane Dione, Gi&aacute;m đốc Quốc gia Ng&acirc;n h&agrave;ng Thế giới tại Việt Nam thăm gian trưng b&agrave;y c&ocirc;ng nghệ xử l&yacute; nước MET tại&nbsp;Triển l&atilde;m C&ocirc;ng nghệ Việt Nam ứng ph&oacute; với biến đổi kh&iacute; hậu, th&aacute;ng 6/2019.</p><p>Với quan niệm m&aacute;y m&oacute;c phải hiện đại, phải c&oacute; c&aacute;c thiết bị bơm nước, sục kh&iacute;, n&eacute;n kh&iacute;, c&aacute;c quy tr&igrave;nh xử l&yacute; bằng h&oacute;a chất v&agrave; vi sinh đầy đủ, nhiều người cho rằng nếu kh&ocirc;ng c&oacute; đầy đủ những yếu tố tr&ecirc;n sẽ kh&ocirc;ng thể xử l&yacute; được nguồn nước &ocirc; nhiễm.</p><p>Theo anh Tiến Anh, nếu giải quyết b&agrave;i to&aacute;n s&ocirc;ng T&ocirc; Lịch bằng c&ocirc;ng nghệ truyền thống th&igrave; chỉ c&oacute; thể xử l&yacute; tập trung, kh&ocirc;ng thể xử l&yacute; rải r&aacute;c, nhưng lại kh&ocirc;ng giải quyết triệt để. C&ograve;n với c&ocirc;ng nghệ MET của C&ocirc;ng ty TA, v&igrave; ưu điểm nhỏ gọn n&ecirc;n c&oacute; thể đặt rải r&aacute;c dọc s&ocirc;ng T&ocirc; Lịch, mỗi module sẽ được đặt ở một miệng cống, lấy nước ra khỏi cống v&agrave; đưa thẳng l&ecirc;n m&aacute;y lọc, nước sau xử l&yacute; sẽ đi thẳng xuống s&ocirc;ng.</p><p>Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA khẳng định: &ldquo;Đảm bảo chỉ chưa đầy nửa năm, s&ocirc;ng T&ocirc; Lịch sẽ hồi sinh ho&agrave;n to&agrave;n&rdquo;.</p><p>&ldquo;Ch&uacute;ng t&ocirc;i xử l&yacute; được từ gốc rồi th&igrave; c&ograve;n đ&acirc;u nước bẩn ra ngo&agrave;i s&ocirc;ng.Với lưu lượng nước ở s&ocirc;ng T&ocirc; Lịch v&agrave; với diện t&iacute;ch hai b&ecirc;n bờ s&ocirc;ng, chắc chắn chỉ cần sử dụng 1/3 diện t&iacute;ch bờ s&ocirc;ng l&agrave; đủ để đặt m&aacute;y m&agrave; kh&ocirc;ng l&agrave;m thay đổi bất cứ kết cấu g&igrave; của s&ocirc;ng&rdquo;.</p><p>Đặc biệt, với c&ocirc;ng nghệ MET, chi ph&iacute; sẽ thấp nhất bởi v&igrave; tiết kiệm đến 90% điện năng ti&ecirc;u thụ, tiết kiệm được 100% h&oacute;a chất, 100% vi sinh v&agrave; tiết kiệm được 90% nh&acirc;n c&ocirc;ng vận h&agrave;nh, cũng như tiết kiệm được chi ph&iacute; đầu tư.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 3’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-3-1563329277-281-width660height746.jpg’ /></p><p>Nh&igrave;n bề ngo&agrave;i, hệ thống lọc nước theo c&ocirc;ng nghệ MET của C&ocirc;ng ty TA kh&aacute; đơn giản nhưng lại rất khoa học.</p><p>C&ocirc;ng nghệ xử l&yacute; n&agrave;y c&oacute; nguy&ecirc;n l&yacute; hoạt động cơ bản gồm: Khi nước v&agrave;o m&aacute;y, đủ &aacute;p lực d&ograve;ng nước chảy qua van hơi tự do v&agrave;o khu vực ph&acirc;n t&aacute;ch, tại đ&acirc;y d&ograve;ng nước bị ph&acirc;n t&aacute;ch th&agrave;nh c&aacute;c tia nhỏ, nước ở dạng ph&acirc;n tử được h&ograve;a trộn với oxy kh&ocirc;ng kh&iacute; tạo kết tủa dạng oxit kim loại lắng lại tr&ecirc;n bề mặt c&aacute;t, phần nước c&ograve;n lại được thấm qua lớp vật liệu xuống đ&aacute;y bể.</p><p>D&ograve;ng nước kh&ocirc;ng bị đẩy ra ngo&agrave;i tiếp tục được ph&acirc;n t&aacute;ch th&agrave;nh dạng bụi nước được xử l&yacute; yếm kh&iacute; trong hệ thống. D&ograve;ng nước thấm xuống đ&aacute;y bể được h&uacute;t ngược trở lại hệ thống để xử l&yacute; nhờ &aacute;p lực d&ograve;ng v&agrave; &aacute;p suất n&eacute;n do nước thấm.</p><p>B&ecirc;n cạnh nguy&ecirc;n l&yacute; cơ học, hệ thống c&ograve;n sử dụng một hỗn hợp chuy&ecirc;n biệt được t&iacute;nh to&aacute;n ri&ecirc;ng theo từng loại nước thải nhằm xử l&yacute; c&aacute;c th&agrave;nh phần &ocirc; nhiễm đặc trưng của loại nước thải đ&oacute;.</p><p>Theo nguy&ecirc;n l&yacute; n&agrave;y, hệ thống cơ vẫn l&agrave; chủ đạo, nhưng n&oacute; h&igrave;nh th&agrave;nh n&ecirc;n l&otilde;i lọc tự nhi&ecirc;n v&agrave; vi sinh yếm kh&iacute;, h&igrave;nh th&agrave;nh n&ecirc;n sự chia t&aacute;ch ph&acirc;n tử v&agrave; t&aacute;ch c&aacute;c chất thải rắn, v&agrave; vẫn h&igrave;nh th&agrave;nh n&ecirc;n hệ thống xử l&yacute; kh&iacute;. Qu&aacute; tr&igrave;nh n&agrave;y mất từ 7-15 ph&uacute;t để c&oacute; được nguồn nước sạch.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 4’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-4-1563329277-702-width660height616.jpg’ /></p><p>Thi c&ocirc;ng hệ thống xử l&yacute; nước tại một doanh nghiệp tư nh&acirc;n do C&ocirc;ng ty TA thực hiện.</p><p><strong>Xử l&yacute; &ocirc; nhiễm c&aacute;c d&ograve;ng s&ocirc;ng chỉ l&agrave; phần ngọn</strong></p><p>Mặc d&ugrave; c&aacute;c kh&aacute;ch h&agrave;ng hiện tại vẫn chủ yếu l&agrave; c&aacute;c doanh nghiệp sản xuất, nhưng anh Vũ Tiến Anh vẫn đau đ&aacute;u với tham vọng hồi sinh những d&ograve;ng s&ocirc;ng đ&atilde; chết v&agrave; coi đ&oacute; l&agrave; l&agrave; tr&aacute;ch nhiệm của m&igrave;nh cần phải đ&oacute;ng g&oacute;p cho x&atilde; hội.</p><p>Tuy nhi&ecirc;n, để hồi sinh những d&ograve;ng s&ocirc;ng như s&ocirc;ng T&ocirc; Lịch, sẽ phải xử l&yacute; từ c&aacute;i gốc trước khi nước được đưa ra d&ograve;ng s&ocirc;ng. N&oacute;i c&aacute;ch kh&aacute;c l&agrave; phải tập trung xử l&yacute; nước thải trong c&aacute;c khu đ&ocirc; thị, trong c&aacute;c nh&agrave; m&aacute;y, trong c&aacute;c khu d&acirc;n cư.</p><p>&ldquo;Chỉ khi l&agrave;m sạch được tất cả nguồn nước đ&oacute; th&igrave; mới ch&iacute;nh thức l&agrave;m sạch được tất cả c&aacute;c d&ograve;ng s&ocirc;ng. Nếu kh&ocirc;ng, việc xử l&yacute; nước ở c&aacute;c d&ograve;ng s&ocirc;ng chỉ l&agrave; phần ngọn m&agrave; kh&ocirc;ng giải quyết được triệt để vấn đề,&rdquo; anh Vũ Tiến Anh n&oacute;i.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 5’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-5-1563329277-38-width660height495.jpg’ /></p><p>Nếu kh&ocirc;ng xử l&yacute; nước thải từ đầu nguồn, việc&nbsp;<a href=‘https://www.24h.com.vn/xu-ly-o-nhiem-song-to-lich-c46e5865.html’ title=‘làm sạch sông Tô Lịch’>l&agrave;m sạch s&ocirc;ng T&ocirc; Lịch</a>&nbsp;chỉ l&agrave; phần th&acirc;n v&agrave; phần ngọn.</p><p>Tại Việt Nam, kh&ocirc;ng thiếu c&aacute;c doanh nghiệp l&agrave;m được việc xử l&yacute; nước thải, nhưng để tối ưu h&oacute;a việc xử l&yacute; nước thải th&igrave; ngay cả tr&ecirc;n thế giới cũng rất &iacute;t doanh nghiệp c&oacute; khả năng l&agrave;m được, bởi tất cả đang đi theo lộ tr&igrave;nh bắt buộc l&agrave; sử dụng điện năng.</p><p>Đ&oacute; l&agrave; l&yacute; do v&igrave; sao TA đạt được c&aacute;c giải thưởng quốc tế như: Huy chương v&agrave;ng v&agrave; Huy chương vinh danh c&ocirc;ng nghệ xanh tốt nhất thế giới tại Nhật Bản, Đ&agrave;i Loan năm 2018; Giải v&agrave;ng Sự kiện 2018 Japan Design, Idea &amp; Invention Expo.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 6’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-6-1563329277-457-width660height334.jpg’ /></p><p>Ở trong nước, phải chờ đợi 3 năm TA mới được cấp bằng s&aacute;ng chế, nhưng cứ ra nước ngo&agrave;i l&agrave; c&oacute; giải thưởng.</p><p>&ldquo;Doanh nghiệp tư nh&acirc;n họ c&oacute; suy nghĩ rất kh&aacute;c, họ lu&ocirc;n t&igrave;m t&ograve;i những c&aacute;i mới, c&oacute; hiệu quả tối ưu th&igrave; họ d&ugrave;ng. Rất may mắn l&agrave; ch&uacute;ng t&ocirc;i đ&atilde; được những doanh nghiệp sản xuất lớn ủng hộ như xi măng Chinfon Hải Ph&ograve;ng, c&ocirc;ng ty nhựa Dismy, gỗ B&igrave;nh Minh, gỗ Ph&uacute;c Hưng, nước mắm Sơn Hải, T&acirc;n Việt &Yacute;, Poshaco,&hellip;&rdquo;</p><p>C&acirc;u chuyện giữa ph&oacute;ng vi&ecirc;n với vị doanh nh&acirc;n trẻ tuổi bị r&uacute;t ngắn hơn so với dự kiến v&igrave; cuộc gọi của một Tổng Gi&aacute;m đốc doanh nghiệp đang l&agrave;m dự &aacute;n nghỉ dưỡng lớn tại Sapa hẹn gặp để đ&agrave;m ph&aacute;n hợp đồng. Vũ Tiến Anh cho biết, nếu đ&agrave;m ph&aacute;n th&agrave;nh c&ocirc;ng, đ&acirc;y sẽ l&agrave; hợp đồng lớn nhất của C&ocirc;ng ty TA.</p>",
                    en_content = "<h2>Anh Tiến Anh Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA khẳng định: &ldquo;Đảm bảo chỉ chưa đầy nửa năm, s&ocirc;ng T&ocirc; Lịch sẽ hồi sinh ho&agrave;n to&agrave;n&rdquo;.</h2><p>Sự kiện:&nbsp;</p><h3><a href=‘https://www.24h.com.vn/xu-ly-o-nhiem-song-to-lich-c46e5865.html’ title=‘Xử lý ô nhiễm sông Tô Lịch’>Xử l&yacute; &ocirc; nhiễm s&ocirc;ng T&ocirc; Lịch</a></h3><p>Đ&oacute; l&agrave; kỹ sư, doanh nh&acirc;n Vũ Tiến Anh, Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA &ndash; với c&ocirc;ng nghệ xử l&yacute; MET (Mechanical Energy Technologies &ndash; C&ocirc;ng nghệ Năng lượng cơ học).</p><p><strong>Chờ đợi 3 năm để được cấp bằng s&aacute;ng chế</strong></p><p>Anh Vũ Tiến Anh hẹn gặp ph&oacute;ng vi&ecirc;n Infonet tại văn ph&ograve;ng C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA (C&ocirc;ng ty TA) v&agrave; cũng l&agrave; nh&agrave; ri&ecirc;ng, trong một con ng&otilde; nhỏ tr&ecirc;n phố T&ocirc;n Đức Thắng, Đống Đa, H&agrave; Nội.</p><p>Tại đ&acirc;y, vị Gi&aacute;m đốc sinh năm 1982 say sưa khoe với ph&oacute;ng vi&ecirc;n về việc c&ocirc;ng nghệ xử l&yacute; nước MET của c&ocirc;ng ty vừa ch&iacute;nh thức được Hội đồng khoa học của Bộ Khoa học v&agrave; C&ocirc;ng nghệ thẩm định, v&agrave; được Cục Sở hữu c&ocirc;ng nghiệp cấp bằng s&aacute;ng chế độc quyền.</p><p>Thay v&igrave; chỉ nhận lắp đặt c&aacute;c c&ocirc;ng tr&igrave;nh xử l&yacute; nước thải ở c&aacute;c doanh nghiệp tư nh&acirc;n v&agrave; c&aacute;c hộ d&acirc;n như trong thời gian qua,&nbsp;c&ocirc;ng ty đ&atilde; c&oacute; đủ tư c&aacute;ch để l&agrave;m c&aacute;c dự &aacute;n xử l&yacute; nước thải cấp bộ, cấp nh&agrave; nước v&agrave; cấp địa phương.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 1’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-1-1563329277-481-width660height495.jpg’ /></p><p>Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA Vũ Tiến Anh, người khởi nghiệp với hệ thống xử l&yacute; nước c&ocirc;ng nghệ MET.</p><p>Anh bảy tỏ, mong muốn trong tương lai gần c&oacute; thể triển khai m&ocirc; h&igrave;nh th&iacute; điểm xử l&yacute; nước đặt tại d&ograve;ng s&ocirc;ng T&ocirc; Lịch (H&agrave; Nội). Đ&acirc;y cũng l&agrave; mong muốn lớn nhất, đ&uacute;ng như ti&ecirc;u ch&iacute; của c&ocirc;ng ty l&agrave; &ldquo;hồi sinh những d&ograve;ng s&ocirc;ng bị &ocirc; nhiễm&rdquo;.</p><p>&quot;Ngay từ năm 2016 khi th&agrave;nh lập doanh nghiệp, t&ocirc;i đ&atilde; đặt m&aacute;y thử nghiệm tại s&ocirc;ng T&ocirc; Lịch, đoạn chảy qua cầu Mới &ndash; Nguyễn Tr&atilde;i. Kết quả cho thấy c&ocirc;ng nghệ xử l&yacute; MET tỏ ra hiệu quả r&otilde; rệt khi nước ban đầu nồng nặc m&ugrave;i h&ocirc;i thối đ&atilde; trở n&ecirc;n trong vắt&quot;.</p><p>&ldquo;Tuy nhi&ecirc;n, suốt từ đ&oacute; đến nay kh&ocirc;ng một ai ghi nhận th&agrave;nh quả của doanh nghiệp, đi đến đ&acirc;u người ta cũng d&egrave; bỉu, họ cho rằng hệ thống n&agrave;y qu&aacute; đơn giản, qu&aacute; th&ocirc; sơ.Đến b&acirc;y giờ, trong suy nghĩ của kh&ocirc;ng chỉ những người ngo&agrave;i ng&agrave;nh m&agrave; cả ở trong ng&agrave;nh, người ta vẫn cho rằng đ&atilde; xử l&yacute; nước thải l&agrave; phải c&oacute; h&oacute;a chất.Họ cho rằng kh&ocirc;ng thể xử l&yacute; được với c&ocirc;ng nghệ của ch&uacute;ng t&ocirc;i&rdquo;, anh Tiến Anh cho hay.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 2’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-2-1563329277-662-width660height495.jpg’ /></p><p>&Ocirc;ng Ousmane Dione, Gi&aacute;m đốc Quốc gia Ng&acirc;n h&agrave;ng Thế giới tại Việt Nam thăm gian trưng b&agrave;y c&ocirc;ng nghệ xử l&yacute; nước MET tại&nbsp;Triển l&atilde;m C&ocirc;ng nghệ Việt Nam ứng ph&oacute; với biến đổi kh&iacute; hậu, th&aacute;ng 6/2019.</p><p>Với quan niệm m&aacute;y m&oacute;c phải hiện đại, phải c&oacute; c&aacute;c thiết bị bơm nước, sục kh&iacute;, n&eacute;n kh&iacute;, c&aacute;c quy tr&igrave;nh xử l&yacute; bằng h&oacute;a chất v&agrave; vi sinh đầy đủ, nhiều người cho rằng nếu kh&ocirc;ng c&oacute; đầy đủ những yếu tố tr&ecirc;n sẽ kh&ocirc;ng thể xử l&yacute; được nguồn nước &ocirc; nhiễm.</p><p>Theo anh Tiến Anh, nếu giải quyết b&agrave;i to&aacute;n s&ocirc;ng T&ocirc; Lịch bằng c&ocirc;ng nghệ truyền thống th&igrave; chỉ c&oacute; thể xử l&yacute; tập trung, kh&ocirc;ng thể xử l&yacute; rải r&aacute;c, nhưng lại kh&ocirc;ng giải quyết triệt để. C&ograve;n với c&ocirc;ng nghệ MET của C&ocirc;ng ty TA, v&igrave; ưu điểm nhỏ gọn n&ecirc;n c&oacute; thể đặt rải r&aacute;c dọc s&ocirc;ng T&ocirc; Lịch, mỗi module sẽ được đặt ở một miệng cống, lấy nước ra khỏi cống v&agrave; đưa thẳng l&ecirc;n m&aacute;y lọc, nước sau xử l&yacute; sẽ đi thẳng xuống s&ocirc;ng.</p><p>Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA khẳng định: &ldquo;Đảm bảo chỉ chưa đầy nửa năm, s&ocirc;ng T&ocirc; Lịch sẽ hồi sinh ho&agrave;n to&agrave;n&rdquo;.</p><p>&ldquo;Ch&uacute;ng t&ocirc;i xử l&yacute; được từ gốc rồi th&igrave; c&ograve;n đ&acirc;u nước bẩn ra ngo&agrave;i s&ocirc;ng.Với lưu lượng nước ở s&ocirc;ng T&ocirc; Lịch v&agrave; với diện t&iacute;ch hai b&ecirc;n bờ s&ocirc;ng, chắc chắn chỉ cần sử dụng 1/3 diện t&iacute;ch bờ s&ocirc;ng l&agrave; đủ để đặt m&aacute;y m&agrave; kh&ocirc;ng l&agrave;m thay đổi bất cứ kết cấu g&igrave; của s&ocirc;ng&rdquo;.</p><p>Đặc biệt, với c&ocirc;ng nghệ MET, chi ph&iacute; sẽ thấp nhất bởi v&igrave; tiết kiệm đến 90% điện năng ti&ecirc;u thụ, tiết kiệm được 100% h&oacute;a chất, 100% vi sinh v&agrave; tiết kiệm được 90% nh&acirc;n c&ocirc;ng vận h&agrave;nh, cũng như tiết kiệm được chi ph&iacute; đầu tư.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 3’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-3-1563329277-281-width660height746.jpg’ /></p><p>Nh&igrave;n bề ngo&agrave;i, hệ thống lọc nước theo c&ocirc;ng nghệ MET của C&ocirc;ng ty TA kh&aacute; đơn giản nhưng lại rất khoa học.</p><p>C&ocirc;ng nghệ xử l&yacute; n&agrave;y c&oacute; nguy&ecirc;n l&yacute; hoạt động cơ bản gồm: Khi nước v&agrave;o m&aacute;y, đủ &aacute;p lực d&ograve;ng nước chảy qua van hơi tự do v&agrave;o khu vực ph&acirc;n t&aacute;ch, tại đ&acirc;y d&ograve;ng nước bị ph&acirc;n t&aacute;ch th&agrave;nh c&aacute;c tia nhỏ, nước ở dạng ph&acirc;n tử được h&ograve;a trộn với oxy kh&ocirc;ng kh&iacute; tạo kết tủa dạng oxit kim loại lắng lại tr&ecirc;n bề mặt c&aacute;t, phần nước c&ograve;n lại được thấm qua lớp vật liệu xuống đ&aacute;y bể.</p><p>D&ograve;ng nước kh&ocirc;ng bị đẩy ra ngo&agrave;i tiếp tục được ph&acirc;n t&aacute;ch th&agrave;nh dạng bụi nước được xử l&yacute; yếm kh&iacute; trong hệ thống. D&ograve;ng nước thấm xuống đ&aacute;y bể được h&uacute;t ngược trở lại hệ thống để xử l&yacute; nhờ &aacute;p lực d&ograve;ng v&agrave; &aacute;p suất n&eacute;n do nước thấm.</p><p>B&ecirc;n cạnh nguy&ecirc;n l&yacute; cơ học, hệ thống c&ograve;n sử dụng một hỗn hợp chuy&ecirc;n biệt được t&iacute;nh to&aacute;n ri&ecirc;ng theo từng loại nước thải nhằm xử l&yacute; c&aacute;c th&agrave;nh phần &ocirc; nhiễm đặc trưng của loại nước thải đ&oacute;.</p><p>Theo nguy&ecirc;n l&yacute; n&agrave;y, hệ thống cơ vẫn l&agrave; chủ đạo, nhưng n&oacute; h&igrave;nh th&agrave;nh n&ecirc;n l&otilde;i lọc tự nhi&ecirc;n v&agrave; vi sinh yếm kh&iacute;, h&igrave;nh th&agrave;nh n&ecirc;n sự chia t&aacute;ch ph&acirc;n tử v&agrave; t&aacute;ch c&aacute;c chất thải rắn, v&agrave; vẫn h&igrave;nh th&agrave;nh n&ecirc;n hệ thống xử l&yacute; kh&iacute;. Qu&aacute; tr&igrave;nh n&agrave;y mất từ 7-15 ph&uacute;t để c&oacute; được nguồn nước sạch.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 4’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-4-1563329277-702-width660height616.jpg’ /></p><p>Thi c&ocirc;ng hệ thống xử l&yacute; nước tại một doanh nghiệp tư nh&acirc;n do C&ocirc;ng ty TA thực hiện.</p><p><strong>Xử l&yacute; &ocirc; nhiễm c&aacute;c d&ograve;ng s&ocirc;ng chỉ l&agrave; phần ngọn</strong></p><p>Mặc d&ugrave; c&aacute;c kh&aacute;ch h&agrave;ng hiện tại vẫn chủ yếu l&agrave; c&aacute;c doanh nghiệp sản xuất, nhưng anh Vũ Tiến Anh vẫn đau đ&aacute;u với tham vọng hồi sinh những d&ograve;ng s&ocirc;ng đ&atilde; chết v&agrave; coi đ&oacute; l&agrave; l&agrave; tr&aacute;ch nhiệm của m&igrave;nh cần phải đ&oacute;ng g&oacute;p cho x&atilde; hội.</p><p>Tuy nhi&ecirc;n, để hồi sinh những d&ograve;ng s&ocirc;ng như s&ocirc;ng T&ocirc; Lịch, sẽ phải xử l&yacute; từ c&aacute;i gốc trước khi nước được đưa ra d&ograve;ng s&ocirc;ng. N&oacute;i c&aacute;ch kh&aacute;c l&agrave; phải tập trung xử l&yacute; nước thải trong c&aacute;c khu đ&ocirc; thị, trong c&aacute;c nh&agrave; m&aacute;y, trong c&aacute;c khu d&acirc;n cư.</p><p>&ldquo;Chỉ khi l&agrave;m sạch được tất cả nguồn nước đ&oacute; th&igrave; mới ch&iacute;nh thức l&agrave;m sạch được tất cả c&aacute;c d&ograve;ng s&ocirc;ng. Nếu kh&ocirc;ng, việc xử l&yacute; nước ở c&aacute;c d&ograve;ng s&ocirc;ng chỉ l&agrave; phần ngọn m&agrave; kh&ocirc;ng giải quyết được triệt để vấn đề,&rdquo; anh Vũ Tiến Anh n&oacute;i.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 5’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-5-1563329277-38-width660height495.jpg’ /></p><p>Nếu kh&ocirc;ng xử l&yacute; nước thải từ đầu nguồn, việc&nbsp;<a href=‘https://www.24h.com.vn/xu-ly-o-nhiem-song-to-lich-c46e5865.html’ title=‘làm sạch sông Tô Lịch’>l&agrave;m sạch s&ocirc;ng T&ocirc; Lịch</a>&nbsp;chỉ l&agrave; phần th&acirc;n v&agrave; phần ngọn.</p><p>Tại Việt Nam, kh&ocirc;ng thiếu c&aacute;c doanh nghiệp l&agrave;m được việc xử l&yacute; nước thải, nhưng để tối ưu h&oacute;a việc xử l&yacute; nước thải th&igrave; ngay cả tr&ecirc;n thế giới cũng rất &iacute;t doanh nghiệp c&oacute; khả năng l&agrave;m được, bởi tất cả đang đi theo lộ tr&igrave;nh bắt buộc l&agrave; sử dụng điện năng.</p><p>Đ&oacute; l&agrave; l&yacute; do v&igrave; sao TA đạt được c&aacute;c giải thưởng quốc tế như: Huy chương v&agrave;ng v&agrave; Huy chương vinh danh c&ocirc;ng nghệ xanh tốt nhất thế giới tại Nhật Bản, Đ&agrave;i Loan năm 2018; Giải v&agrave;ng Sự kiện 2018 Japan Design, Idea &amp; Invention Expo.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 6’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-6-1563329277-457-width660height334.jpg’ /></p><p>Ở trong nước, phải chờ đợi 3 năm TA mới được cấp bằng s&aacute;ng chế, nhưng cứ ra nước ngo&agrave;i l&agrave; c&oacute; giải thưởng.</p><p>&ldquo;Doanh nghiệp tư nh&acirc;n họ c&oacute; suy nghĩ rất kh&aacute;c, họ lu&ocirc;n t&igrave;m t&ograve;i những c&aacute;i mới, c&oacute; hiệu quả tối ưu th&igrave; họ d&ugrave;ng. Rất may mắn l&agrave; ch&uacute;ng t&ocirc;i đ&atilde; được những doanh nghiệp sản xuất lớn ủng hộ như xi măng Chinfon Hải Ph&ograve;ng, c&ocirc;ng ty nhựa Dismy, gỗ B&igrave;nh Minh, gỗ Ph&uacute;c Hưng, nước mắm Sơn Hải, T&acirc;n Việt &Yacute;, Poshaco,&hellip;&rdquo;</p><p>C&acirc;u chuyện giữa ph&oacute;ng vi&ecirc;n với vị doanh nh&acirc;n trẻ tuổi bị r&uacute;t ngắn hơn so với dự kiến v&igrave; cuộc gọi của một Tổng Gi&aacute;m đốc doanh nghiệp đang l&agrave;m dự &aacute;n nghỉ dưỡng lớn tại Sapa hẹn gặp để đ&agrave;m ph&aacute;n hợp đồng. Vũ Tiến Anh cho biết, nếu đ&agrave;m ph&aacute;n th&agrave;nh c&ocirc;ng, đ&acirc;y sẽ l&agrave; hợp đồng lớn nhất của C&ocirc;ng ty TA.</p>",
                    date_create = DateTime.Now,
                    active = true,
                    id_admin = 1,
                    view = 6,
                    id_department = 1,
                    img = "new1.jpg"
                },
                new News
                {
                    id = 2,
                    vi_title = "Adidas ra sản phẩm mới",
                    en_title = "Shoe is put on by pair",
                    vi_content = "<h2>Anh Tiến Anh Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA khẳng định: &ldquo;Đảm bảo chỉ chưa đầy nửa năm, s&ocirc;ng T&ocirc; Lịch sẽ hồi sinh ho&agrave;n to&agrave;n&rdquo;.</h2><p>Sự kiện:&nbsp;</p><h3><a href=‘https://www.24h.com.vn/xu-ly-o-nhiem-song-to-lich-c46e5865.html’ title=‘Xử lý ô nhiễm sông Tô Lịch’>Xử l&yacute; &ocirc; nhiễm s&ocirc;ng T&ocirc; Lịch</a></h3><p>Đ&oacute; l&agrave; kỹ sư, doanh nh&acirc;n Vũ Tiến Anh, Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA &ndash; với c&ocirc;ng nghệ xử l&yacute; MET (Mechanical Energy Technologies &ndash; C&ocirc;ng nghệ Năng lượng cơ học).</p><p><strong>Chờ đợi 3 năm để được cấp bằng s&aacute;ng chế</strong></p><p>Anh Vũ Tiến Anh hẹn gặp ph&oacute;ng vi&ecirc;n Infonet tại văn ph&ograve;ng C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA (C&ocirc;ng ty TA) v&agrave; cũng l&agrave; nh&agrave; ri&ecirc;ng, trong một con ng&otilde; nhỏ tr&ecirc;n phố T&ocirc;n Đức Thắng, Đống Đa, H&agrave; Nội.</p><p>Tại đ&acirc;y, vị Gi&aacute;m đốc sinh năm 1982 say sưa khoe với ph&oacute;ng vi&ecirc;n về việc c&ocirc;ng nghệ xử l&yacute; nước MET của c&ocirc;ng ty vừa ch&iacute;nh thức được Hội đồng khoa học của Bộ Khoa học v&agrave; C&ocirc;ng nghệ thẩm định, v&agrave; được Cục Sở hữu c&ocirc;ng nghiệp cấp bằng s&aacute;ng chế độc quyền.</p><p>Thay v&igrave; chỉ nhận lắp đặt c&aacute;c c&ocirc;ng tr&igrave;nh xử l&yacute; nước thải ở c&aacute;c doanh nghiệp tư nh&acirc;n v&agrave; c&aacute;c hộ d&acirc;n như trong thời gian qua,&nbsp;c&ocirc;ng ty đ&atilde; c&oacute; đủ tư c&aacute;ch để l&agrave;m c&aacute;c dự &aacute;n xử l&yacute; nước thải cấp bộ, cấp nh&agrave; nước v&agrave; cấp địa phương.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 1’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-1-1563329277-481-width660height495.jpg’ /></p><p>Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA Vũ Tiến Anh, người khởi nghiệp với hệ thống xử l&yacute; nước c&ocirc;ng nghệ MET.</p><p>Anh bảy tỏ, mong muốn trong tương lai gần c&oacute; thể triển khai m&ocirc; h&igrave;nh th&iacute; điểm xử l&yacute; nước đặt tại d&ograve;ng s&ocirc;ng T&ocirc; Lịch (H&agrave; Nội). Đ&acirc;y cũng l&agrave; mong muốn lớn nhất, đ&uacute;ng như ti&ecirc;u ch&iacute; của c&ocirc;ng ty l&agrave; &ldquo;hồi sinh những d&ograve;ng s&ocirc;ng bị &ocirc; nhiễm&rdquo;.</p><p>&quot;Ngay từ năm 2016 khi th&agrave;nh lập doanh nghiệp, t&ocirc;i đ&atilde; đặt m&aacute;y thử nghiệm tại s&ocirc;ng T&ocirc; Lịch, đoạn chảy qua cầu Mới &ndash; Nguyễn Tr&atilde;i. Kết quả cho thấy c&ocirc;ng nghệ xử l&yacute; MET tỏ ra hiệu quả r&otilde; rệt khi nước ban đầu nồng nặc m&ugrave;i h&ocirc;i thối đ&atilde; trở n&ecirc;n trong vắt&quot;.</p><p>&ldquo;Tuy nhi&ecirc;n, suốt từ đ&oacute; đến nay kh&ocirc;ng một ai ghi nhận th&agrave;nh quả của doanh nghiệp, đi đến đ&acirc;u người ta cũng d&egrave; bỉu, họ cho rằng hệ thống n&agrave;y qu&aacute; đơn giản, qu&aacute; th&ocirc; sơ.Đến b&acirc;y giờ, trong suy nghĩ của kh&ocirc;ng chỉ những người ngo&agrave;i ng&agrave;nh m&agrave; cả ở trong ng&agrave;nh, người ta vẫn cho rằng đ&atilde; xử l&yacute; nước thải l&agrave; phải c&oacute; h&oacute;a chất.Họ cho rằng kh&ocirc;ng thể xử l&yacute; được với c&ocirc;ng nghệ của ch&uacute;ng t&ocirc;i&rdquo;, anh Tiến Anh cho hay.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 2’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-2-1563329277-662-width660height495.jpg’ /></p><p>&Ocirc;ng Ousmane Dione, Gi&aacute;m đốc Quốc gia Ng&acirc;n h&agrave;ng Thế giới tại Việt Nam thăm gian trưng b&agrave;y c&ocirc;ng nghệ xử l&yacute; nước MET tại&nbsp;Triển l&atilde;m C&ocirc;ng nghệ Việt Nam ứng ph&oacute; với biến đổi kh&iacute; hậu, th&aacute;ng 6/2019.</p><p>Với quan niệm m&aacute;y m&oacute;c phải hiện đại, phải c&oacute; c&aacute;c thiết bị bơm nước, sục kh&iacute;, n&eacute;n kh&iacute;, c&aacute;c quy tr&igrave;nh xử l&yacute; bằng h&oacute;a chất v&agrave; vi sinh đầy đủ, nhiều người cho rằng nếu kh&ocirc;ng c&oacute; đầy đủ những yếu tố tr&ecirc;n sẽ kh&ocirc;ng thể xử l&yacute; được nguồn nước &ocirc; nhiễm.</p><p>Theo anh Tiến Anh, nếu giải quyết b&agrave;i to&aacute;n s&ocirc;ng T&ocirc; Lịch bằng c&ocirc;ng nghệ truyền thống th&igrave; chỉ c&oacute; thể xử l&yacute; tập trung, kh&ocirc;ng thể xử l&yacute; rải r&aacute;c, nhưng lại kh&ocirc;ng giải quyết triệt để. C&ograve;n với c&ocirc;ng nghệ MET của C&ocirc;ng ty TA, v&igrave; ưu điểm nhỏ gọn n&ecirc;n c&oacute; thể đặt rải r&aacute;c dọc s&ocirc;ng T&ocirc; Lịch, mỗi module sẽ được đặt ở một miệng cống, lấy nước ra khỏi cống v&agrave; đưa thẳng l&ecirc;n m&aacute;y lọc, nước sau xử l&yacute; sẽ đi thẳng xuống s&ocirc;ng.</p><p>Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA khẳng định: &ldquo;Đảm bảo chỉ chưa đầy nửa năm, s&ocirc;ng T&ocirc; Lịch sẽ hồi sinh ho&agrave;n to&agrave;n&rdquo;.</p><p>&ldquo;Ch&uacute;ng t&ocirc;i xử l&yacute; được từ gốc rồi th&igrave; c&ograve;n đ&acirc;u nước bẩn ra ngo&agrave;i s&ocirc;ng.Với lưu lượng nước ở s&ocirc;ng T&ocirc; Lịch v&agrave; với diện t&iacute;ch hai b&ecirc;n bờ s&ocirc;ng, chắc chắn chỉ cần sử dụng 1/3 diện t&iacute;ch bờ s&ocirc;ng l&agrave; đủ để đặt m&aacute;y m&agrave; kh&ocirc;ng l&agrave;m thay đổi bất cứ kết cấu g&igrave; của s&ocirc;ng&rdquo;.</p><p>Đặc biệt, với c&ocirc;ng nghệ MET, chi ph&iacute; sẽ thấp nhất bởi v&igrave; tiết kiệm đến 90% điện năng ti&ecirc;u thụ, tiết kiệm được 100% h&oacute;a chất, 100% vi sinh v&agrave; tiết kiệm được 90% nh&acirc;n c&ocirc;ng vận h&agrave;nh, cũng như tiết kiệm được chi ph&iacute; đầu tư.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 3’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-3-1563329277-281-width660height746.jpg’ /></p><p>Nh&igrave;n bề ngo&agrave;i, hệ thống lọc nước theo c&ocirc;ng nghệ MET của C&ocirc;ng ty TA kh&aacute; đơn giản nhưng lại rất khoa học.</p><p>C&ocirc;ng nghệ xử l&yacute; n&agrave;y c&oacute; nguy&ecirc;n l&yacute; hoạt động cơ bản gồm: Khi nước v&agrave;o m&aacute;y, đủ &aacute;p lực d&ograve;ng nước chảy qua van hơi tự do v&agrave;o khu vực ph&acirc;n t&aacute;ch, tại đ&acirc;y d&ograve;ng nước bị ph&acirc;n t&aacute;ch th&agrave;nh c&aacute;c tia nhỏ, nước ở dạng ph&acirc;n tử được h&ograve;a trộn với oxy kh&ocirc;ng kh&iacute; tạo kết tủa dạng oxit kim loại lắng lại tr&ecirc;n bề mặt c&aacute;t, phần nước c&ograve;n lại được thấm qua lớp vật liệu xuống đ&aacute;y bể.</p><p>D&ograve;ng nước kh&ocirc;ng bị đẩy ra ngo&agrave;i tiếp tục được ph&acirc;n t&aacute;ch th&agrave;nh dạng bụi nước được xử l&yacute; yếm kh&iacute; trong hệ thống. D&ograve;ng nước thấm xuống đ&aacute;y bể được h&uacute;t ngược trở lại hệ thống để xử l&yacute; nhờ &aacute;p lực d&ograve;ng v&agrave; &aacute;p suất n&eacute;n do nước thấm.</p><p>B&ecirc;n cạnh nguy&ecirc;n l&yacute; cơ học, hệ thống c&ograve;n sử dụng một hỗn hợp chuy&ecirc;n biệt được t&iacute;nh to&aacute;n ri&ecirc;ng theo từng loại nước thải nhằm xử l&yacute; c&aacute;c th&agrave;nh phần &ocirc; nhiễm đặc trưng của loại nước thải đ&oacute;.</p><p>Theo nguy&ecirc;n l&yacute; n&agrave;y, hệ thống cơ vẫn l&agrave; chủ đạo, nhưng n&oacute; h&igrave;nh th&agrave;nh n&ecirc;n l&otilde;i lọc tự nhi&ecirc;n v&agrave; vi sinh yếm kh&iacute;, h&igrave;nh th&agrave;nh n&ecirc;n sự chia t&aacute;ch ph&acirc;n tử v&agrave; t&aacute;ch c&aacute;c chất thải rắn, v&agrave; vẫn h&igrave;nh th&agrave;nh n&ecirc;n hệ thống xử l&yacute; kh&iacute;. Qu&aacute; tr&igrave;nh n&agrave;y mất từ 7-15 ph&uacute;t để c&oacute; được nguồn nước sạch.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 4’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-4-1563329277-702-width660height616.jpg’ /></p><p>Thi c&ocirc;ng hệ thống xử l&yacute; nước tại một doanh nghiệp tư nh&acirc;n do C&ocirc;ng ty TA thực hiện.</p><p><strong>Xử l&yacute; &ocirc; nhiễm c&aacute;c d&ograve;ng s&ocirc;ng chỉ l&agrave; phần ngọn</strong></p><p>Mặc d&ugrave; c&aacute;c kh&aacute;ch h&agrave;ng hiện tại vẫn chủ yếu l&agrave; c&aacute;c doanh nghiệp sản xuất, nhưng anh Vũ Tiến Anh vẫn đau đ&aacute;u với tham vọng hồi sinh những d&ograve;ng s&ocirc;ng đ&atilde; chết v&agrave; coi đ&oacute; l&agrave; l&agrave; tr&aacute;ch nhiệm của m&igrave;nh cần phải đ&oacute;ng g&oacute;p cho x&atilde; hội.</p><p>Tuy nhi&ecirc;n, để hồi sinh những d&ograve;ng s&ocirc;ng như s&ocirc;ng T&ocirc; Lịch, sẽ phải xử l&yacute; từ c&aacute;i gốc trước khi nước được đưa ra d&ograve;ng s&ocirc;ng. N&oacute;i c&aacute;ch kh&aacute;c l&agrave; phải tập trung xử l&yacute; nước thải trong c&aacute;c khu đ&ocirc; thị, trong c&aacute;c nh&agrave; m&aacute;y, trong c&aacute;c khu d&acirc;n cư.</p><p>&ldquo;Chỉ khi l&agrave;m sạch được tất cả nguồn nước đ&oacute; th&igrave; mới ch&iacute;nh thức l&agrave;m sạch được tất cả c&aacute;c d&ograve;ng s&ocirc;ng. Nếu kh&ocirc;ng, việc xử l&yacute; nước ở c&aacute;c d&ograve;ng s&ocirc;ng chỉ l&agrave; phần ngọn m&agrave; kh&ocirc;ng giải quyết được triệt để vấn đề,&rdquo; anh Vũ Tiến Anh n&oacute;i.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 5’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-5-1563329277-38-width660height495.jpg’ /></p><p>Nếu kh&ocirc;ng xử l&yacute; nước thải từ đầu nguồn, việc&nbsp;<a href=‘https://www.24h.com.vn/xu-ly-o-nhiem-song-to-lich-c46e5865.html’ title=‘làm sạch sông Tô Lịch’>l&agrave;m sạch s&ocirc;ng T&ocirc; Lịch</a>&nbsp;chỉ l&agrave; phần th&acirc;n v&agrave; phần ngọn.</p><p>Tại Việt Nam, kh&ocirc;ng thiếu c&aacute;c doanh nghiệp l&agrave;m được việc xử l&yacute; nước thải, nhưng để tối ưu h&oacute;a việc xử l&yacute; nước thải th&igrave; ngay cả tr&ecirc;n thế giới cũng rất &iacute;t doanh nghiệp c&oacute; khả năng l&agrave;m được, bởi tất cả đang đi theo lộ tr&igrave;nh bắt buộc l&agrave; sử dụng điện năng.</p><p>Đ&oacute; l&agrave; l&yacute; do v&igrave; sao TA đạt được c&aacute;c giải thưởng quốc tế như: Huy chương v&agrave;ng v&agrave; Huy chương vinh danh c&ocirc;ng nghệ xanh tốt nhất thế giới tại Nhật Bản, Đ&agrave;i Loan năm 2018; Giải v&agrave;ng Sự kiện 2018 Japan Design, Idea &amp; Invention Expo.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 6’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-6-1563329277-457-width660height334.jpg’ /></p><p>Ở trong nước, phải chờ đợi 3 năm TA mới được cấp bằng s&aacute;ng chế, nhưng cứ ra nước ngo&agrave;i l&agrave; c&oacute; giải thưởng.</p><p>&ldquo;Doanh nghiệp tư nh&acirc;n họ c&oacute; suy nghĩ rất kh&aacute;c, họ lu&ocirc;n t&igrave;m t&ograve;i những c&aacute;i mới, c&oacute; hiệu quả tối ưu th&igrave; họ d&ugrave;ng. Rất may mắn l&agrave; ch&uacute;ng t&ocirc;i đ&atilde; được những doanh nghiệp sản xuất lớn ủng hộ như xi măng Chinfon Hải Ph&ograve;ng, c&ocirc;ng ty nhựa Dismy, gỗ B&igrave;nh Minh, gỗ Ph&uacute;c Hưng, nước mắm Sơn Hải, T&acirc;n Việt &Yacute;, Poshaco,&hellip;&rdquo;</p><p>C&acirc;u chuyện giữa ph&oacute;ng vi&ecirc;n với vị doanh nh&acirc;n trẻ tuổi bị r&uacute;t ngắn hơn so với dự kiến v&igrave; cuộc gọi của một Tổng Gi&aacute;m đốc doanh nghiệp đang l&agrave;m dự &aacute;n nghỉ dưỡng lớn tại Sapa hẹn gặp để đ&agrave;m ph&aacute;n hợp đồng. Vũ Tiến Anh cho biết, nếu đ&agrave;m ph&aacute;n th&agrave;nh c&ocirc;ng, đ&acirc;y sẽ l&agrave; hợp đồng lớn nhất của C&ocirc;ng ty TA.</p>",
                    en_content = "Shoe must be wore by pair, it's so stupid ",
                    date_create = DateTime.Now,
                    active = true,
                    id_admin = 2,
                    view = 8,
                    id_department = 1,
                    img = "new2.jpg"
                },
                new News
                {
                    id = 4,
                    vi_title = "Nike Phá Sản nếu không tham gia Exxaxi",
                    en_title = "Shoe is put on by pair",
                    vi_content = "<h2>Anh Tiến Anh Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA khẳng định: &ldquo;Đảm bảo chỉ chưa đầy nửa năm, s&ocirc;ng T&ocirc; Lịch sẽ hồi sinh ho&agrave;n to&agrave;n&rdquo;.</h2><p>Sự kiện:&nbsp;</p><h3><a href=‘https://www.24h.com.vn/xu-ly-o-nhiem-song-to-lich-c46e5865.html’ title=‘Xử lý ô nhiễm sông Tô Lịch’>Xử l&yacute; &ocirc; nhiễm s&ocirc;ng T&ocirc; Lịch</a></h3><p>Đ&oacute; l&agrave; kỹ sư, doanh nh&acirc;n Vũ Tiến Anh, Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA &ndash; với c&ocirc;ng nghệ xử l&yacute; MET (Mechanical Energy Technologies &ndash; C&ocirc;ng nghệ Năng lượng cơ học).</p><p><strong>Chờ đợi 3 năm để được cấp bằng s&aacute;ng chế</strong></p><p>Anh Vũ Tiến Anh hẹn gặp ph&oacute;ng vi&ecirc;n Infonet tại văn ph&ograve;ng C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA (C&ocirc;ng ty TA) v&agrave; cũng l&agrave; nh&agrave; ri&ecirc;ng, trong một con ng&otilde; nhỏ tr&ecirc;n phố T&ocirc;n Đức Thắng, Đống Đa, H&agrave; Nội.</p><p>Tại đ&acirc;y, vị Gi&aacute;m đốc sinh năm 1982 say sưa khoe với ph&oacute;ng vi&ecirc;n về việc c&ocirc;ng nghệ xử l&yacute; nước MET của c&ocirc;ng ty vừa ch&iacute;nh thức được Hội đồng khoa học của Bộ Khoa học v&agrave; C&ocirc;ng nghệ thẩm định, v&agrave; được Cục Sở hữu c&ocirc;ng nghiệp cấp bằng s&aacute;ng chế độc quyền.</p><p>Thay v&igrave; chỉ nhận lắp đặt c&aacute;c c&ocirc;ng tr&igrave;nh xử l&yacute; nước thải ở c&aacute;c doanh nghiệp tư nh&acirc;n v&agrave; c&aacute;c hộ d&acirc;n như trong thời gian qua,&nbsp;c&ocirc;ng ty đ&atilde; c&oacute; đủ tư c&aacute;ch để l&agrave;m c&aacute;c dự &aacute;n xử l&yacute; nước thải cấp bộ, cấp nh&agrave; nước v&agrave; cấp địa phương.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 1’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-1-1563329277-481-width660height495.jpg’ /></p><p>Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA Vũ Tiến Anh, người khởi nghiệp với hệ thống xử l&yacute; nước c&ocirc;ng nghệ MET.</p><p>Anh bảy tỏ, mong muốn trong tương lai gần c&oacute; thể triển khai m&ocirc; h&igrave;nh th&iacute; điểm xử l&yacute; nước đặt tại d&ograve;ng s&ocirc;ng T&ocirc; Lịch (H&agrave; Nội). Đ&acirc;y cũng l&agrave; mong muốn lớn nhất, đ&uacute;ng như ti&ecirc;u ch&iacute; của c&ocirc;ng ty l&agrave; &ldquo;hồi sinh những d&ograve;ng s&ocirc;ng bị &ocirc; nhiễm&rdquo;.</p><p>&quot;Ngay từ năm 2016 khi th&agrave;nh lập doanh nghiệp, t&ocirc;i đ&atilde; đặt m&aacute;y thử nghiệm tại s&ocirc;ng T&ocirc; Lịch, đoạn chảy qua cầu Mới &ndash; Nguyễn Tr&atilde;i. Kết quả cho thấy c&ocirc;ng nghệ xử l&yacute; MET tỏ ra hiệu quả r&otilde; rệt khi nước ban đầu nồng nặc m&ugrave;i h&ocirc;i thối đ&atilde; trở n&ecirc;n trong vắt&quot;.</p><p>&ldquo;Tuy nhi&ecirc;n, suốt từ đ&oacute; đến nay kh&ocirc;ng một ai ghi nhận th&agrave;nh quả của doanh nghiệp, đi đến đ&acirc;u người ta cũng d&egrave; bỉu, họ cho rằng hệ thống n&agrave;y qu&aacute; đơn giản, qu&aacute; th&ocirc; sơ.Đến b&acirc;y giờ, trong suy nghĩ của kh&ocirc;ng chỉ những người ngo&agrave;i ng&agrave;nh m&agrave; cả ở trong ng&agrave;nh, người ta vẫn cho rằng đ&atilde; xử l&yacute; nước thải l&agrave; phải c&oacute; h&oacute;a chất.Họ cho rằng kh&ocirc;ng thể xử l&yacute; được với c&ocirc;ng nghệ của ch&uacute;ng t&ocirc;i&rdquo;, anh Tiến Anh cho hay.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 2’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-2-1563329277-662-width660height495.jpg’ /></p><p>&Ocirc;ng Ousmane Dione, Gi&aacute;m đốc Quốc gia Ng&acirc;n h&agrave;ng Thế giới tại Việt Nam thăm gian trưng b&agrave;y c&ocirc;ng nghệ xử l&yacute; nước MET tại&nbsp;Triển l&atilde;m C&ocirc;ng nghệ Việt Nam ứng ph&oacute; với biến đổi kh&iacute; hậu, th&aacute;ng 6/2019.</p><p>Với quan niệm m&aacute;y m&oacute;c phải hiện đại, phải c&oacute; c&aacute;c thiết bị bơm nước, sục kh&iacute;, n&eacute;n kh&iacute;, c&aacute;c quy tr&igrave;nh xử l&yacute; bằng h&oacute;a chất v&agrave; vi sinh đầy đủ, nhiều người cho rằng nếu kh&ocirc;ng c&oacute; đầy đủ những yếu tố tr&ecirc;n sẽ kh&ocirc;ng thể xử l&yacute; được nguồn nước &ocirc; nhiễm.</p><p>Theo anh Tiến Anh, nếu giải quyết b&agrave;i to&aacute;n s&ocirc;ng T&ocirc; Lịch bằng c&ocirc;ng nghệ truyền thống th&igrave; chỉ c&oacute; thể xử l&yacute; tập trung, kh&ocirc;ng thể xử l&yacute; rải r&aacute;c, nhưng lại kh&ocirc;ng giải quyết triệt để. C&ograve;n với c&ocirc;ng nghệ MET của C&ocirc;ng ty TA, v&igrave; ưu điểm nhỏ gọn n&ecirc;n c&oacute; thể đặt rải r&aacute;c dọc s&ocirc;ng T&ocirc; Lịch, mỗi module sẽ được đặt ở một miệng cống, lấy nước ra khỏi cống v&agrave; đưa thẳng l&ecirc;n m&aacute;y lọc, nước sau xử l&yacute; sẽ đi thẳng xuống s&ocirc;ng.</p><p>Gi&aacute;m đốc C&ocirc;ng ty TNHH C&ocirc;ng nghệ xử l&yacute; nước TA khẳng định: &ldquo;Đảm bảo chỉ chưa đầy nửa năm, s&ocirc;ng T&ocirc; Lịch sẽ hồi sinh ho&agrave;n to&agrave;n&rdquo;.</p><p>&ldquo;Ch&uacute;ng t&ocirc;i xử l&yacute; được từ gốc rồi th&igrave; c&ograve;n đ&acirc;u nước bẩn ra ngo&agrave;i s&ocirc;ng.Với lưu lượng nước ở s&ocirc;ng T&ocirc; Lịch v&agrave; với diện t&iacute;ch hai b&ecirc;n bờ s&ocirc;ng, chắc chắn chỉ cần sử dụng 1/3 diện t&iacute;ch bờ s&ocirc;ng l&agrave; đủ để đặt m&aacute;y m&agrave; kh&ocirc;ng l&agrave;m thay đổi bất cứ kết cấu g&igrave; của s&ocirc;ng&rdquo;.</p><p>Đặc biệt, với c&ocirc;ng nghệ MET, chi ph&iacute; sẽ thấp nhất bởi v&igrave; tiết kiệm đến 90% điện năng ti&ecirc;u thụ, tiết kiệm được 100% h&oacute;a chất, 100% vi sinh v&agrave; tiết kiệm được 90% nh&acirc;n c&ocirc;ng vận h&agrave;nh, cũng như tiết kiệm được chi ph&iacute; đầu tư.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 3’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-3-1563329277-281-width660height746.jpg’ /></p><p>Nh&igrave;n bề ngo&agrave;i, hệ thống lọc nước theo c&ocirc;ng nghệ MET của C&ocirc;ng ty TA kh&aacute; đơn giản nhưng lại rất khoa học.</p><p>C&ocirc;ng nghệ xử l&yacute; n&agrave;y c&oacute; nguy&ecirc;n l&yacute; hoạt động cơ bản gồm: Khi nước v&agrave;o m&aacute;y, đủ &aacute;p lực d&ograve;ng nước chảy qua van hơi tự do v&agrave;o khu vực ph&acirc;n t&aacute;ch, tại đ&acirc;y d&ograve;ng nước bị ph&acirc;n t&aacute;ch th&agrave;nh c&aacute;c tia nhỏ, nước ở dạng ph&acirc;n tử được h&ograve;a trộn với oxy kh&ocirc;ng kh&iacute; tạo kết tủa dạng oxit kim loại lắng lại tr&ecirc;n bề mặt c&aacute;t, phần nước c&ograve;n lại được thấm qua lớp vật liệu xuống đ&aacute;y bể.</p><p>D&ograve;ng nước kh&ocirc;ng bị đẩy ra ngo&agrave;i tiếp tục được ph&acirc;n t&aacute;ch th&agrave;nh dạng bụi nước được xử l&yacute; yếm kh&iacute; trong hệ thống. D&ograve;ng nước thấm xuống đ&aacute;y bể được h&uacute;t ngược trở lại hệ thống để xử l&yacute; nhờ &aacute;p lực d&ograve;ng v&agrave; &aacute;p suất n&eacute;n do nước thấm.</p><p>B&ecirc;n cạnh nguy&ecirc;n l&yacute; cơ học, hệ thống c&ograve;n sử dụng một hỗn hợp chuy&ecirc;n biệt được t&iacute;nh to&aacute;n ri&ecirc;ng theo từng loại nước thải nhằm xử l&yacute; c&aacute;c th&agrave;nh phần &ocirc; nhiễm đặc trưng của loại nước thải đ&oacute;.</p><p>Theo nguy&ecirc;n l&yacute; n&agrave;y, hệ thống cơ vẫn l&agrave; chủ đạo, nhưng n&oacute; h&igrave;nh th&agrave;nh n&ecirc;n l&otilde;i lọc tự nhi&ecirc;n v&agrave; vi sinh yếm kh&iacute;, h&igrave;nh th&agrave;nh n&ecirc;n sự chia t&aacute;ch ph&acirc;n tử v&agrave; t&aacute;ch c&aacute;c chất thải rắn, v&agrave; vẫn h&igrave;nh th&agrave;nh n&ecirc;n hệ thống xử l&yacute; kh&iacute;. Qu&aacute; tr&igrave;nh n&agrave;y mất từ 7-15 ph&uacute;t để c&oacute; được nguồn nước sạch.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 4’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-4-1563329277-702-width660height616.jpg’ /></p><p>Thi c&ocirc;ng hệ thống xử l&yacute; nước tại một doanh nghiệp tư nh&acirc;n do C&ocirc;ng ty TA thực hiện.</p><p><strong>Xử l&yacute; &ocirc; nhiễm c&aacute;c d&ograve;ng s&ocirc;ng chỉ l&agrave; phần ngọn</strong></p><p>Mặc d&ugrave; c&aacute;c kh&aacute;ch h&agrave;ng hiện tại vẫn chủ yếu l&agrave; c&aacute;c doanh nghiệp sản xuất, nhưng anh Vũ Tiến Anh vẫn đau đ&aacute;u với tham vọng hồi sinh những d&ograve;ng s&ocirc;ng đ&atilde; chết v&agrave; coi đ&oacute; l&agrave; l&agrave; tr&aacute;ch nhiệm của m&igrave;nh cần phải đ&oacute;ng g&oacute;p cho x&atilde; hội.</p><p>Tuy nhi&ecirc;n, để hồi sinh những d&ograve;ng s&ocirc;ng như s&ocirc;ng T&ocirc; Lịch, sẽ phải xử l&yacute; từ c&aacute;i gốc trước khi nước được đưa ra d&ograve;ng s&ocirc;ng. N&oacute;i c&aacute;ch kh&aacute;c l&agrave; phải tập trung xử l&yacute; nước thải trong c&aacute;c khu đ&ocirc; thị, trong c&aacute;c nh&agrave; m&aacute;y, trong c&aacute;c khu d&acirc;n cư.</p><p>&ldquo;Chỉ khi l&agrave;m sạch được tất cả nguồn nước đ&oacute; th&igrave; mới ch&iacute;nh thức l&agrave;m sạch được tất cả c&aacute;c d&ograve;ng s&ocirc;ng. Nếu kh&ocirc;ng, việc xử l&yacute; nước ở c&aacute;c d&ograve;ng s&ocirc;ng chỉ l&agrave; phần ngọn m&agrave; kh&ocirc;ng giải quyết được triệt để vấn đề,&rdquo; anh Vũ Tiến Anh n&oacute;i.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 5’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-5-1563329277-38-width660height495.jpg’ /></p><p>Nếu kh&ocirc;ng xử l&yacute; nước thải từ đầu nguồn, việc&nbsp;<a href=‘https://www.24h.com.vn/xu-ly-o-nhiem-song-to-lich-c46e5865.html’ title=‘làm sạch sông Tô Lịch’>l&agrave;m sạch s&ocirc;ng T&ocirc; Lịch</a>&nbsp;chỉ l&agrave; phần th&acirc;n v&agrave; phần ngọn.</p><p>Tại Việt Nam, kh&ocirc;ng thiếu c&aacute;c doanh nghiệp l&agrave;m được việc xử l&yacute; nước thải, nhưng để tối ưu h&oacute;a việc xử l&yacute; nước thải th&igrave; ngay cả tr&ecirc;n thế giới cũng rất &iacute;t doanh nghiệp c&oacute; khả năng l&agrave;m được, bởi tất cả đang đi theo lộ tr&igrave;nh bắt buộc l&agrave; sử dụng điện năng.</p><p>Đ&oacute; l&agrave; l&yacute; do v&igrave; sao TA đạt được c&aacute;c giải thưởng quốc tế như: Huy chương v&agrave;ng v&agrave; Huy chương vinh danh c&ocirc;ng nghệ xanh tốt nhất thế giới tại Nhật Bản, Đ&agrave;i Loan năm 2018; Giải v&agrave;ng Sự kiện 2018 Japan Design, Idea &amp; Invention Expo.</p><p><img alt=‘Một doanh nhân trẻ Hà Nội cam kết hồi sinh vĩnh viễn dòng sông Tô Lịch - 6’ src=‘https://cdn.24h.com.vn/upload/3-2019/images/2019-07-17/Mot-doanh-nhan-tre-Ha-Noi-cam-ket-hoi-sinh-vinh-vien-dong-song-To-Lich-to-lich-6-1563329277-457-width660height334.jpg’ /></p><p>Ở trong nước, phải chờ đợi 3 năm TA mới được cấp bằng s&aacute;ng chế, nhưng cứ ra nước ngo&agrave;i l&agrave; c&oacute; giải thưởng.</p><p>&ldquo;Doanh nghiệp tư nh&acirc;n họ c&oacute; suy nghĩ rất kh&aacute;c, họ lu&ocirc;n t&igrave;m t&ograve;i những c&aacute;i mới, c&oacute; hiệu quả tối ưu th&igrave; họ d&ugrave;ng. Rất may mắn l&agrave; ch&uacute;ng t&ocirc;i đ&atilde; được những doanh nghiệp sản xuất lớn ủng hộ như xi măng Chinfon Hải Ph&ograve;ng, c&ocirc;ng ty nhựa Dismy, gỗ B&igrave;nh Minh, gỗ Ph&uacute;c Hưng, nước mắm Sơn Hải, T&acirc;n Việt &Yacute;, Poshaco,&hellip;&rdquo;</p><p>C&acirc;u chuyện giữa ph&oacute;ng vi&ecirc;n với vị doanh nh&acirc;n trẻ tuổi bị r&uacute;t ngắn hơn so với dự kiến v&igrave; cuộc gọi của một Tổng Gi&aacute;m đốc doanh nghiệp đang l&agrave;m dự &aacute;n nghỉ dưỡng lớn tại Sapa hẹn gặp để đ&agrave;m ph&aacute;n hợp đồng. Vũ Tiến Anh cho biết, nếu đ&agrave;m ph&aacute;n th&agrave;nh c&ocirc;ng, đ&acirc;y sẽ l&agrave; hợp đồng lớn nhất của C&ocirc;ng ty TA.</p>",
                    en_content = "Shoe must be wore by pair, it's so stupid ",
                    date_create = DateTime.Now,
                    active = true,
                    id_admin = 2,
                    view = 9,
                    id_department = 1,
                    img = "new3.jpg"
                },
                new News
                {
                    id = 3,
                    vi_title = "Thời Trang Châu Á có bước thay đổi lớn",
                    en_title = "Clothes of Genos Brand is hold 100% marketplace of Asia",
                    vi_content = "Genos đẹp trai nên nắm giữ toàn bộ thị trường đúng rồi khỏi thắc mắc gì thêm",
                    en_content = "Genos is so handsome, he hold all marketplace is tangibility ",
                    date_create = DateTime.Now,
                    active = true,
                    id_admin = 3,
                    view = 96,
                    id_department = 2,
                    img = "new4.jpg"
                }
            );
            builder.Entity<Posts>().HasData(
                new Posts
                {

                    id = 1,
                    price = 3400000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 1,
                    id_size = 1,
                    id_user = 1,
                    id_city = 1,
                    status = 0
                },
                new Posts
                {
                    id = 2,
                    price = 2900000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 0,
                    id_size = 1,
                    id_user = 2,
                    id_city = 2,
                    status = 0
                },
                new Posts
                {

                    id = 3,
                    price = 3500000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 1,
                    id_size = 2,
                    id_user = 3,
                    id_city = 3,
                    status = 0
                },
                new Posts
                {
                    id = 4,
                    price = 2800000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 0,
                    id_size = 2,
                    id_user = 1,
                    id_city = 2,
                    status = 0
                },
                new Posts
                {
                    id = 5,
                    price = 4000000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 1,
                    id_size = 3,
                    id_user = 4,
                    id_city = 1,
                    status = 0
                },
                new Posts
                {
                    id = 6,
                    price = 1000000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 0,
                    id_size = 3,
                    id_user = 2,
                    id_city = 1,
                    status = 0
                },
                new Posts
                {
                    id = 7,
                    price = 3900000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 0,
                    id_size = 4,
                    id_user = 1,
                    id_city = 2,
                    status = 0
                },
                new Posts
                {
                    id = 8,
                    price = 4900000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 1,
                    id_size = 4,
                    id_user = 2,
                    id_city = 3,
                    status = 0
                },
                new Posts
                {
                    id = 9,
                    price = 6000000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 1,
                    id_size = 5,
                    id_user = 4,
                    id_city = 3,
                    status = 0
                },
                new Posts
                {
                    id = 10,
                    price = 2550000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 0,
                    id_size = 5,
                    id_user = 1,
                    id_city = 2,
                    status = 0
                },
                new Posts
                 {
                     id = 11,
                     price = 5900000,
                     date_start = DateTime.Parse("2019-01-05"),
                     date_end = DateTime.Parse("2019-12-12"),
                     kind = 1,
                     id_size = 6,
                     id_user = 1,
                     id_city = 1,
                     status = 0
                 },
                new Posts
                 {
                     id = 12,
                     price = 4950000,
                     date_start = DateTime.Parse("2019-01-05"),
                     date_end = DateTime.Parse("2019-12-12"),
                     kind = 0,
                     id_size = 6,
                     id_user = 4,
                    id_city = 1,
                    status = 0
                 },
                new Posts
                {
                    id = 13,
                    price = 3600000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 0,
                    id_size =7,
                    id_user = 4,
                    id_city = 2,
                    status = 0
                },
                new Posts
                {
                    id = 14,
                    price = 6000000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 1,
                    id_size = 7,
                    id_user = 1,
                    id_city = 3,
                    status = 0
                },
                new Posts
                {
                    id = 15,
                    price = 4400000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 1,
                    id_size = 8,
                    id_user = 1,
                    id_city = 3,
                    status = 0
                },
                new Posts
                {
                    id = 16,
                    price = 3900000,
                    date_start = DateTime.Parse("2019-01-05"),
                    date_end = DateTime.Parse("2019-12-12"),
                    kind = 0,
                    id_size = 8,
                    id_user = 2,
                    id_city = 2,
                    status = 0
                },
                new Posts
                 {
                     id = 17,
                     price = 5300000,
                     date_start = DateTime.Parse("2019-01-05"),
                     date_end = DateTime.Parse("2019-12-12"),
                     kind = 1,
                     id_size = 9,
                     id_user = 4,
                    id_city = 1,
                    status = 0
                 },
                new Posts
                 {
                     id = 18,
                     price = 2250000,
                     date_start = DateTime.Parse("2019-01-05"),
                     date_end = DateTime.Parse("2019-12-12"),
                     kind = 0,
                     id_size = 9,
                     id_user = 1,
                     id_city = 1,
                     status = 0
                 }
            );
            builder.Entity<Followings>().HasData(
                new Followings
                {
                    id = 1,
                    id_size = 2,
                    id_user = 1
                },
                new Followings
                {
                    id = 2,
                    id_size = 1,
                    id_user = 1
                },
                new Followings
                {
                    id = 3,
                    id_size = 1,
                    id_user = 2
                }
            );
            builder.Entity<Shippings>().HasData(
                new Shippings
                {
                    id = 1,
                    name = "Miền Nam",
                    shipping_fee = 10000
                },
                new Shippings
                {
                    id = 2,
                    name = "Miền Trung",
                    shipping_fee = 20000
                },
                new Shippings
                {
                    id = 3,
                    name = "Miền Bắc",
                    shipping_fee = 30000
                }
            );
            builder.Entity<Citys>().HasData(
                new Citys
                {
                    id = 1,
                    name = "Tp Hồ Chí Minh",
                    id_shipping = 1
                },
                new Citys
                {
                    id = 2,
                    name = "Huế",
                    id_shipping = 2
                },
                new Citys
                {
                    id = 3,
                    name = "Hà Nội",
                    id_shipping = 3
                }
            );
            builder.Entity<ServiceFeeDetails>().HasData(
                new ServiceFeeDetails
                {
                    id = 1,
                    level = "1",
                    sale_required = 0,
                    value = 0.098
                },
                new ServiceFeeDetails
                {
                    id = 2,
                    level = "2",
                    sale_required = 3,
                    value = 0.09
                },
                new ServiceFeeDetails
                {
                    id = 3,
                    level = "3",
                    sale_required = 30,
                    value = 0.085
                },
                new ServiceFeeDetails
                {
                    id = 4,
                    level = "4",
                    sale_required = 100,
                    value = 0.08
                }
            );
        }

        internal Task FindByIdAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
