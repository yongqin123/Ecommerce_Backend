using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ItemId))]
        public int ItemId { get; set; } = 0;

        [Required]
        public string Email { get; set; } = "";

        [Required]
        public int Quantity { get; set; } = 0;

        [Required]
        public string Size { get; set; } = "";
    }
}
