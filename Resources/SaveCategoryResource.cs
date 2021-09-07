using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.Resources
{
    public class SaveCategoryResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}