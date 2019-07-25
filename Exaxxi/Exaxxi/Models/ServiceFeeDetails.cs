using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.Models
{
    public class ServiceFeeDetails
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Cấp")]
        [Required]
        public string level { get; set; }
        [Display(Name ="Số Hóa Đơn Yêu Cầu")]
        [Required]
        public int sale_required { get; set; }
        [Display(Name = "Giá trị")]
        [Required]
        [Range(0, 1)]
        public double value { get; set; }
    }
}
