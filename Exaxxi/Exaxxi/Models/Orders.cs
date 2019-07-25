using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.Models
{
    public class Orders
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Start Time")]
        public DateTime time { get; set; }
        [Required]
        [Display(Name = "Địa Chỉ Giao Hàng")]
        public string address { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Số Điện Thoại")]
        public string phone { get; set; }
        [Display(Name = "Phí Giao Hàng")]
        public double ship_fee { get; set; }
        [Display(Name = "Phí Dịch Vụ")]
        public double service_fee { get; set; }
        [Display(Name = "Phương Thức Thanh Toán")]
        public int payment { get; set; }
        [Display(Name = "Mã Giảm Giá")]
        public string id_voucher { get; set; }
        [Display(Name = "Giá Tiền")]
        public double price { get; set; }
        [Display(Name = "Trạng Thái")]
        public int status { get; set; }
        [Display(Name = "Số Tiền Được Giảm")]
        public double discount { get; set; }
        [Display(Name = "ID User Order")]
        public int id_user { get; set; }
        public int id_post { get; set; }
        public int id_city { get; set; }
        [ForeignKey("id_city")]
        public Citys city { get; set; }
    }
}
