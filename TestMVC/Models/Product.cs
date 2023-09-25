using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(25)]
        [MinLength(2,ErrorMessage ="Name must be more than 2 char")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Range(minimum:2000,maximum:200000)]
        public int Price { get; set; }
        [RegularExpression(@"\w{1,}\.(jpg|png)", ErrorMessage = "Image must be jpg or png")]
        public string? Image { get; set; }
        [ForeignKey("Category")]
        public int CatId { get; set; }
        public int? CurrentPrice { get; set;}
        public Category? Category { get; set; }
    }
}
