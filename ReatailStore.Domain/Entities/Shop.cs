﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReatailStore.Domain.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}
