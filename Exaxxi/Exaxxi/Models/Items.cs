using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exaxxi.Models
{
    public class Items
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [Required]
        [MaxLength(50,ErrorMessage = "tối đa 50 kí tự")]
        public string name { get; set; }
        [Display(Name = "Tên thư mục")]
        [Required]
        [MaxLength(50,ErrorMessage = "tối đa 50 kí tự")]
        public string folder { get; set; }
        [Display(Name ="Thông tin sản phẩm tiếng việt")]
        [DataType(DataType.Text)]
        public string vi_info { get; set; }
        [Display(Name = "Thông tin sản phẩm tiếng anh")]
        [DataType(DataType.Text)]
        public string en_info { get; set; }
        [Display(Name = "Giới Tính")]
        public Gender gender { get; set; }
        [Required]
        [Display(Name = "Hình")]
        public string img { get; set; }
        [Required]
        [Display(Name = "Hình 3d")]
        public string img3d { get; set; }
        [Display(Name = "Biến động giá")]
        public float volatility { get; set; }
        [Display(Name = "Giá giao dịch thấp nhất")]
        public double trade_min { get; set; }
        [Display(Name = "Giá giao dịch cao nhất")]
        public double trade_max { get; set; }
        public bool active { get; set; }
        // lowest and highest from all sizes this item
        public double? lowest_ask { get; set; }
        public double? highest_bid { get; set; }
        public int sold { get; set; }

        public int id_admin { get; set; }
        [ForeignKey("id_admin")]
        public Admins admin { get; set; }
        public int id_category { get; set; }
        [ForeignKey("id_category")]
        public Categories category { get; set; }

        //public IEnumerable<Sizes> sizes { get; set; }

    }
    public enum Gender
    {
        [Description("Nam")]
        Male = 0,
        [Description("Nữ")]
        Female = 1
    }
}