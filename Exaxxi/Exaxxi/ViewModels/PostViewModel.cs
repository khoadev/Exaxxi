using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.Models
{
    public class PostViewModel
    {
        public Items item { get; set; }
        public int id_post { get; set; }
        public DateTime create_at { get; set; }
        public string cate_name { get; set; }
        public string brand_name { get; set; }
        public double price { get; set; }
        public int kind { get; set; }
        public Sizes size { get; set; }
        public ds_Size dsSize { get; set; }
        public Posts post { get; set; }
    }
}
