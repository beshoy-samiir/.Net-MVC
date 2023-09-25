using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TestMVC.Models;

namespace TestMVC.ViewModel
{
    public class ProductWithCategoryListViewModel
    {
        public int Id { get; set; }

        [MaxLength(25)]
        [MinLength(3, ErrorMessage = "Name must be more than 2 char")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Range(minimum: 2000, maximum: 20000)]
        public int Price { get; set; }

        [RegularExpression(@"\w{1,}\.(jpg|png)", ErrorMessage = "Image must be jpg or png")]
        public string Image { get; set; }

        [DisplayName("Category")]
        public int CatId { get; set; }

        public List<Category> Categories { get; set; }
    }
}
