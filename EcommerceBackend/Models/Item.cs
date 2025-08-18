using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Gender { get; set; } = "";

        [Required]
        public string Path { get; set; } = "";

        [Required]
        public decimal Price { get; set; } = 0;
    }
}
