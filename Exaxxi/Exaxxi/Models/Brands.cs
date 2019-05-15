using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exaxxi.Models
{
    public class Brands
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Tên thương hiệu")]
        [Required]
        [MaxLength(50, ErrorMessage = "tối đa 50 kí tự")]
        public string name { get; set; }
        public bool active { get; set; }
        public int order { get; set; }

        public int id_department { get; set; }
        [ForeignKey("id_department")]
        public Departments department { get; set; }

        //public IEnumerable<Categories> categories { get; set; }
    }
}