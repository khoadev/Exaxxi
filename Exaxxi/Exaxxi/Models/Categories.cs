using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exaxxi.Models
{
    public class Categories
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Tên loại tiếng việt")]
        [Required]
        [MaxLength(50, ErrorMessage = "tối đa 50 kí tự")]
        public string vi_name { get; set; }
  
        public bool active { get; set; }
        [Display(Name = "Thứ tự sắp xếp")]
        public int order { get; set; }

        public int id_brand { get; set; }
        [ForeignKey("id_brand")]
        public Brands brand { get; set; }

        public IEnumerable<Items> items { get; set; }

        internal object Include(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}