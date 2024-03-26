using System.ComponentModel.DataAnnotations;

namespace Pet_Shop_Management.Models
{
    public class Login
    {
        [Key]
        [Required]
        [MinLength(8)]
        [MaxLength(30)]
        [RegularExpression(@"^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4})*$", ErrorMessage = "Please enter Valid Email Address")]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(.{8,20})$",
   ErrorMessage = "Password must contain a Capital letter and a Number withing range 8 to 20 ")]
        public string Password { get; set; }

    }
}
