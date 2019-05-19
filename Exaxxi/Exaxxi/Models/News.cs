using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exaxxi.Models
{
    public class News
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Tiêu đề tiếng việt")]
        //[MaxLength(50,ErrorMessage = "tối đa 50 kí tự")]
        [Required]
        public string vi_title { get; set; }
        [Display(Name = "Tiêu đề tiếng anh")]
        //[MaxLength(50, ErrorMessage = "tối đa 50 kí tự")]
        [Required]
        public string en_title { get; set; }
        [Display(Name = "Hình")]
        [MaxLength(150, ErrorMessage = "Tối đa 150 kí tự")]
        public string image { get; set; }
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Nội dung tiếng việt")]
        public string vi_content { get; set; }
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Nội dung tiếng anh")]
        public string en_content { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày đăng")]
        public DateTime date_create { get; set; }
        public bool active { get; set; }
        public int id_admin { get; set; }
        [ForeignKey("id_admin")]
        public Admins admin { get; set; }
        public int id_department { get; set; }
        [ForeignKey("id_department")]
        public Departments department { get; set; }
    }
}