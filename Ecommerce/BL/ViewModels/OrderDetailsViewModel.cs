using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int Quantity { get; set; }
        
        [Required]
        [ForeignKey("Order")]
        [Display(Name ="Order")]
        public int Order_Id { get; set; }

        [Required]
        [ForeignKey("Product")]
        [Display(Name = "Product")]
        public int Product_Id { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
