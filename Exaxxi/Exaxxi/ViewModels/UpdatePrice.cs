using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.ViewModels
{
    public class UpdatePrice
    {
        public int id { get; set; }
        public double lowest_ask { get; set; }
        public double highest_bid { get; set; }
    }
}
