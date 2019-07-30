using Exaxxi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.ViewModels
{
    public class OrderViewModel
    {
        public Items items { get; set; }
        public Orders orders { get; set; }
        public Sizes size { get; set; }
    }
}
