using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels
{
    public class ProductColorDTO
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
     
        public int ProductId { get; set; }
    }
}
