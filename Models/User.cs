using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pet_Shop_Management.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        [Remote("UserExistsName", "UserRegister", HttpMethod = "POST", ErrorMessage = "User with this Name Already exists")]


        public string Name { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(30)]
        [Remote("UserExistsEmail", "UserRegister", HttpMethod = "POST", ErrorMessage = "User with this Email Already exists")]
        [RegularExpression(@"^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4})*$", ErrorMessage = "Please enter Valid Email Address")]
        public string Email { get; set; }
        [Required]

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(.{8,50})$",
        ErrorMessage = "Password must contain a Capital letter and a Number withing range 8 to 20 ")]
        public string Password { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(40)]
        public string Address { get; set; }
        public string Role { get; set; }
    }
}
