using Exaxxi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Tên")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Mật khẩu")]
        //[MaxLength(30 , ErrorMessage = "Tối đa 30 kí tự")]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$",
        ErrorMessage = "Tối thiểu 8 kí tự, có ít nhất 1 chữ thường, 1 chữ in hoa và 1 kí tự số")]
        public string password { get; set; }
        [Display(Name = "Nhập lại mật khẩu")]
        [MaxLength(30, ErrorMessage = "Tối đa 30 kí tự")]
        [Required]
        [Compare("password")]
        public string confirm_password { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        [Required]
        public string email { get; set; }
        [Display(Name = "Số điện thoại")]
        [Phone]
        public string phone { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string address { get; set; }
        public int id_city { get; set; }
        [ForeignKey("id_city")]
        public Citys city { get; set; }
        public string captcha { get; set; }

        public Users toUsers()
        {
            return new Users { name = this.name, password = this.password, email = this.email, phone = this.phone, address = this.address, id_city = this.id_city, active = true, num_item_selled = 0, level_seller = 1, score_buyer = 0, date_registion = DateTime.Now };
        }
    }
    
}
