using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels
{
    public class ProductSizeDTO
    {
        public int Id { get; set; }
        public string SizeName { get; set; }
        public decimal SizePrice { get; set; }
        public int ProductId { get; set; }
    }
}
