using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";
        
        [Required]
        public string Email { get; set; } = "";
        
        [Required]
        public string Password { get; set; } = "";
        
        [Required]
        public string Phone { get; set; } = "";

        [Required]
        public string AccountType { get; set; } = "";

        [Required]
        public string Address { get; set; } = "";

        [Required]
        public string City { get; set; } = "";

    }
}
