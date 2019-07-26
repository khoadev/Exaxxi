using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.Models
{
    public class Citys
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Tên tỉnh")]
        [Required]
        public string name { get; set; }
        public int id_shipping { get; set; }
        [ForeignKey("id_shipping")]
        public Shippings shipping { get; set; }
    }
}
