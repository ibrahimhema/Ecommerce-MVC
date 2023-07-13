using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
   public class Wallet
    {
        public int Id { get; set; }
        [DefaultValue(0)]
        public decimal my_balance { set; get; }
        [DefaultValue("جنيه مصري")]
        public string currunce { set; get; }
        [DefaultValue(0)]
        public decimal withdrawn_balance { set; get; }
        [Required]
        public string User_Id { get; set; }
      
        [ForeignKey("User_Id")]
       
        public virtual ApplicationUser User { set; get; }


    }
}
