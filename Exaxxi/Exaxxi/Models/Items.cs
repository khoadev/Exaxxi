using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exaxxi.Models
{
    public class Items
    {
        [Key]
        int id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [Required]
        [MaxLength(50,ErrorMessage = "tối đa 50 kí tự")]
        public string name { get; set; }
        [Display(Name ="Thông tin sản phẩm")]
        [DataType(DataType.Text)]
        public string info { get; set; }
        [Required]
        [Display(Name = "Hình")]
        [Url]
        public string img { get; set; }
        [Display(Name = "Biến động giá")]
        public float volatility { get; set; }
        [Display(Name = "Giá giao dịch thấp nhất")]
        public double trade_min { get; set; }
        [Display(Name = "Giá giao dịch cao nhất")]
        public double trade_max { get; set; }
        public bool active { get; set; }

        public int id_admin { get; set; }
        [ForeignKey("id")]
        public Admins admin { get; set; }
        public int id_category { get; set; }
        [ForeignKey("id")]
        public Categories category { get; set; }

        public IEnumerable<Sizes> sizes { get; set; }

    }
}