using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Đăng Nhập")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhớ Mật Khẩu")]
        public bool RememberMe { get; set; }
    }
}
