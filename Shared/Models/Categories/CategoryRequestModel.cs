
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models.Categories
{
    public class CategoryRequestModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }
    }
}
