using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exaxxi.Models
{
    public class ds_Size
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Size Việt Nam")]
        [Required]
        [Range(15,100, ErrorMessage = "Nằm trong khoảng 15-100")]
        public int VN { get; set; }
        [Display(Name = "Size Mỹ")]
        [Required]
        [Range(0, 100, ErrorMessage ="Nằm trong khoảng 0-100")]
        public float US { get; set; }
        [Display(Name = "Size Anh")]
        [Required]
        [Range(0, 100, ErrorMessage = "Nằm trong khoảng 0-100")]
        public float UK { get; set; }
        [Display(Name = "Inch")]
        [Required]
        [Range(3, 100, ErrorMessage = "Nằm trong khoảng 3-100")]
        public float Inch { get; set; }
        [Display(Name = "Centimet")]
        [Required]
        [Range(7, 100, ErrorMessage = "Nằm trong khoảng 7-100")]
        public float Centimet { get; set; }
        public int id_Depart { get; set; }
    }
}