﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exaxxi.Models
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Tên")]
        [MaxLength(50 , ErrorMessage = "Tối đa 50 kí tự")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", 
        ErrorMessage = "Tối thiểu 8 kí tự, có ít nhất 1 chữ thường, 1 chữ in hoa và 1 kí tự số")] 
        public string password { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        [Required]
        public string email { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string phone { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        //[Display(Name = "size giày")]
        //public ds_Size shoe_size { get; set; }
        //[Display(Name = "Đơn vị tiền tệ")]
        //string Currency { get; set; }
        [Display(Name = "Number Item User Selled")]
        public int num_item_selled { get; set; }
        [Display(Name = "Cấp độ người bán")]
        [Range(1,4)]
        public int level_seller { get; set; }
        [ForeignKey("level_seller")]
        public ServiceFeeDetails service_fee { get; set; }
        [Display(Name = "Điểm người mua")]
        [Range(0,5)]
        public int score_buyer { get; set; }
        [Display(Name = "Thời gian đăng ký")]
        [DataType(DataType.DateTime)]
        public DateTime date_registion { get; set; }
        public bool active { get; set; }
        [Required]
        public int id_city { get; set; }
        

        //public IEnumerable<Followings> followings { get; set; }
        //public IEnumerable<Posts> posts { get; set; }
    }
}