using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models.DTOs
{
    public class ItemUploadDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Gender { get; set; } = "";

        [Required]
        public IFormFile Path { get; set; }

        [Required]
        public decimal Price { get; set; } = 0;
    }
}
