using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.Models
{
    public class Voucher
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Code Khuyen Mai")]
        [Required]
        public string code { get; set; }
        //kind 1 giam theo %,kind 2 giam thang gia tien
        [Display(Name = "Loại Voucher")]
        [Required]
        public int kind { get; set; }
        [Required]
        public double value { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime date_start { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime date_end { get; set; }
        [Display(Name = "Số Lần Sử Dụng")]
        [Required]
        public int count { get; set; }

    }
}
