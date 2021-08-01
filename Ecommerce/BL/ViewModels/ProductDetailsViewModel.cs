using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels
{
  public class ProductDetailsViewModel
    {
        public ProductViewModel product { set; get; }
        public List<ProductViewModel> products { set; get; }
    }
}
