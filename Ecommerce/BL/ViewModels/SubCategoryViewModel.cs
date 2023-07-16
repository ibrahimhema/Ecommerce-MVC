using System.ComponentModel.DataAnnotations;

namespace BL.ViewModels
{
    public class SubCategoryViewModel
    {
        public int? Id { set; get; }
        [Required]
        public string Name { set; get; }


        public string ParentCatName { set; get; }
        public string MainCatName { set; get; }

        public int? Parent_Id { set; get; }
        [Required]
        public int Cat_Id { set; get; }

        public string Photo { set; get; }
        public bool Active { set; get; }
   
    }
}
