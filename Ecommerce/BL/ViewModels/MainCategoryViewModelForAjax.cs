using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels
{
   public class MainCategoryViewModelForAjax
    {
       public int? Id { set; get; }
        [Required]
        public string Name { set; get; }
        public string Photo { set; get; }
        public bool Active { set; get; }
    }
}
