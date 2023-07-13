using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public enum Notfication_Stutes
    {
        Seen,Notseen
    }
  public class Notifiication
    {
        public Notifiication()
        {
            Created_at = DateTime.Now;
            Stutes = Notfication_Stutes.Notseen;
        }
        public int Id { get; set; }
        [Required]
        public string Desc { set; get; }
        [Required]
        public DateTime Created_at { set; get; }
        [Required]
        public int ProductId { set; get; }
        [Required]
        public string Vendor_Id { get; set; }
        public Notfication_Stutes Stutes { set; get; }

    }
}
