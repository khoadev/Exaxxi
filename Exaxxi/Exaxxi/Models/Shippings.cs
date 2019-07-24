using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.Models
{
    public class Shippings
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Tên khu vực")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Phí giao hàng")]
        [Required]
        public double shipping_fee { get; set; }
    }
}
