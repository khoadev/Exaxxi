﻿using Exaxxi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.ViewModels
{
    public class PostViewAdmin
    {
       
        public Posts post { get; set; }
        public int size { get; set; }
        public string nameItem { get; set; }
        public string usernameBuy { get; set; }
        public string usernameSell { get; set; }
        public Orders order { get; set; }
    }
}
