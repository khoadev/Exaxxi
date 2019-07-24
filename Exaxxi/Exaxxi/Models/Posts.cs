using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exaxxi.Models
{
    public class Posts
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Giá")]
        public double price { get; set; }
        [Display(Name = "Tên Khách Hàng")]
        public string name_client { get; set; }
        [Display(Name = "Số Điện Thoại")]
        [Phone]
        public string phone { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string address { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime date_start { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime date_end { get; set; }
        [Range(1, 2)]
        [Display(Name = "Hình thức giao dịch")]
        public int kind { get; set; }
        public int status { get; set; }
        public int id_size { get; set; }
        [ForeignKey("id_size")]
        public Sizes size { get; set; }
        public int id_user { get; set; }
        [ForeignKey("id_user")]
        public Users user { get; set; } 
        public int id_city { get; set; }
        [ForeignKey("id_city")]
        public Citys city { get; set; }
    }
}