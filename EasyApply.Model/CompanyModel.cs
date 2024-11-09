using System.ComponentModel.DataAnnotations;

namespace EasyApply.Model
{
    public class CompanyModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }
    }
}