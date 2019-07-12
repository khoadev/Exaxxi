using Exaxxi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.ViewModels
{
    public class ItemViewAdmin
    {
        public Items items { get; set; }
        [Display(Name = "Tên Admin")]
        public string nameAdmin { get; set; }
        [Display(Name = "Tên Cate")]
        public string nameCate { get; set; }
    }
}
