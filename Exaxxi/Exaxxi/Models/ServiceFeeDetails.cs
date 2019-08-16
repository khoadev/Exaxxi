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
        [Display(Name = "Level")]
        [Required]
        public int level { get; set; }
        [Display(Name ="Sale Requested")]
        [Required]
        public int sale_required { get; set; }
        [Display(Name = "Money Sale Requested")]
        public double money_sale_required { get; set; }
        [Display(Name = "Value")]
        [Required]
        [Range(0, 1)]
        public double value { get; set; }
    }
}
