using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class ProductImages
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string PhotoURL { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        public Product Product { get; set; }
    }
}
