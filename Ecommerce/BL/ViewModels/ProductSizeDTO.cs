﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels
{
    public class ProductSizeDTO
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
    }
}
