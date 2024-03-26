
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Shop_Management.Models
{
    public class Cart
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cart_Id { get; set; }

        public int Pet_Id { get; set; }

        public int User_Id { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("User_Id")]
        public virtual User user { get; set; }

        [ForeignKey("Pet_Id")]

        public virtual Pet Pet { get; set; }
    }
}

