using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(18)]
        public decimal Price { get; set; }
        [MaxLength(18)]
        public string FileName { get; set; }
    }
}