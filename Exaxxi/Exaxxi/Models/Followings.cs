using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exaxxi.Models
{
    public class Followings
    {
        [Key]
        public int id { get; set; }
        public int id_user { get; set; }
        [ForeignKey("id_user")]
        public Users user { get; set; }
        public int id_size { get; set; }
        [ForeignKey("id_size")]
        public Sizes wish { get; set; }
    }
}