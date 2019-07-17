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
        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime date_start { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime date_end { get; set; }
        [Range(0, 1)]
        [Display(Name = "Hình thức giao dịch")]
        public int kind { get; set; }
        public int status { get; set; }
        public int id_size { get; set; }
        [ForeignKey("id_size")]
        public Sizes size { get; set; }
        public int id_user { get; set; }
        [ForeignKey("id_user")]
        public Users user { get; set; } 
    }
}