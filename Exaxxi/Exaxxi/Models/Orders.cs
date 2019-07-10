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
        [Display(Name = "Delivery Address")]
        public string address { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string phone { get; set; }
        [Display(Name = "Status")]
        public int status { get; set; }

        public int id_user { get; set; }
        public int id_post { get; set; }
    }
}
