using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pet_Shop_Management.Models
{
    public class Pet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Pet_Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{4,15}$",
         ErrorMessage = "Please enter Alphabets Only Within Range 4 to 15 Alphabets")]
        public string Pet_name { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{1,12}$",
        ErrorMessage = "Please enter digits only")]
        public int Pet_price { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{4,90}$",
         ErrorMessage = "Please enter Alphabets Only Within Range 4 to 400 Alphabets")]
        public string Pet_description { get; set; }


        [Display(Name = "Choose the  photo of your Pet")]
        [NotMapped]

        public IFormFile? Pet_Photo { get; set; }
        public string Pet_Image_Url { get; set; }
        public string? Director { get; set; }
        public string? Actor { get; set; }



        public DateTime Pet_created_date { get; set; } = DateTime.Now;


        public bool IsDeleted { get; set; } = false;

        public int created_By_Id { get; set; }


    }
}
