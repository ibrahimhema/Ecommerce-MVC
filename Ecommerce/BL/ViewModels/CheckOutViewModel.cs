using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels
{
    public class CheckOutViewModel
    {
        public int Id { get; set; }
        [DefaultValue(Order_Status.Pending)]
        public Order_Status Status { get; set; }
        [Display(Name = "Total")]
        public decimal Total_Price { get; set; }

        [Required]
        public string Address { get; set; }

        
        public DateTime Created_at { get; set; }
        [Required]
        [Display(Name = "User")]
        public string User_Id { get; set; }
      

        [Display(Name = "Payment Method")]
        public int Payment_Id { get; set; }

       // public int payment { get; set; }


        public int? RowId { set; get; }
        public List<Order_Product> Products { get; set; }

        //order Details 

    }
}
