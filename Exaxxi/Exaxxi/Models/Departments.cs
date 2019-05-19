using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exaxxi.Models
{
    public class Departments
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Tên chủng loại tiếng việt")]
        [Required]
        [MaxLength(50, ErrorMessage = "tối đa 50 kí tự")]
        public string vi_name { get; set; }
        [Display(Name = "Tên chủng loại tiếng anh")]
        [Required]
        [MaxLength(50, ErrorMessage = "tối đa 50 kí tự")]
        public string en_name { get; set; }
        public bool active { get; set; }
        public int order { get; set; }

        //public IEnumerable<Brands> brands { get; set; }
        //public IEnumerable<News> news { get; set; }
    }
}