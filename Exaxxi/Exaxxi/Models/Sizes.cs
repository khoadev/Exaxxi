using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exaxxi.Models
{
    public class Sizes
    {
        [Key]
        public int id { get; set; }
        // lowest and highest from all posts of this size
        public double lowest_ask { get; set; }
        public double highest_bid { get; set; }
        public double last_sale {get;set; }

        public int id_ds_size { get; set; }
        [ForeignKey("id_ds_size")]
        public ds_Size size { get; set; }

        public int id_item { get; set; }
        [ForeignKey("id_item")]
        public Items item { get; set; }

        //public IEnumerable<Posts> posts { get; set; }
        //public IEnumerable<Followings> followings { get; set; }
    }
}