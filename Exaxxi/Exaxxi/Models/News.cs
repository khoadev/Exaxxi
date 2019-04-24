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
        [Display(Name = "Tiêu đề")]
        [MaxLength(50,ErrorMessage = "tối đa 50 kí tự")]
        [Required]
        public string title { get; set; }
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Nội dung")]
        public string content { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày đăng")]
        public DateTime date_create { get; set; }
        public bool active { get; set; }
        public int id_admin { get; set; }
        [ForeignKey("id")]
        public Admins admin { get; set; }
        public int id_department { get; set; }
        [ForeignKey("id")]
        public Departments department { get; set; }
    }
}