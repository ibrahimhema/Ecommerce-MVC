using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels
{
    public class ProfileEditViewModel
    {
        [Required]
        [MaxLength(20)]
        [Display(Name = "First name")]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Last name")]
        [MinLength(3)]
        public string LastName { get; set; }

        public string Photo { get; set; }

        public string Address { get; set; }
    }
}
