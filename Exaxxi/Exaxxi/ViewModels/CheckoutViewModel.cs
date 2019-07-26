using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.ViewModels
{
    public class CheckoutViewModel
    {
        public string Account { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int id_city { get; set; }
        public string shipping { get; set; }
        public double total { get; set; }
        public string Payment { get; set; }
        public double Enter_bid { get; set; }
        public double Enter_ask { get; set; }
        public string Exp_Day { get; set; }
        public int? id_voucher { get; set; }
        public double? discount { get; set; }
    }
}
