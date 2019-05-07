using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exaxxi.Models
{
    public class Admins
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "tối đa 50 kí tự")]
        [Display(Name = "Tên tài khoản")]
        public string username { get; set; }
        [Display(Name = "Mật khẩu")]
        [MaxLength(30, ErrorMessage = "Tối đa 30 kí tự")]
        [Required]
        [RegularExpression("^(?=.*[a - z])(?=.*[A - Z])(?=.*\\d)[a - zA - Z\\d]{8,}$")]
        public string password { get; set; }
        [Display(Name = "Nhập lại mật khẩu")]
        [MaxLength(30, ErrorMessage = "Tối đa 30 kí tự")]
        [Required]
        [Compare("password")]
        private string confirm_password { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        [Required]
        public string email { get; set; }
        public int level { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Thời gian tạo")]
        public DateTime date_create { get; set; }
        public bool active { get; set; }

        public IEnumerable<Sizes> sizes { get; set; }
        public IEnumerable<Items> items { get; set; }
        public IEnumerable<News> news { get; set; }
    }
}