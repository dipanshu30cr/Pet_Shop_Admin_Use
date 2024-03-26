using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Shop_Management.Models
{
    public class Booking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Invoice { get; set; }

        public int UserId { get; set; }

        public int Pet_Id { get; set; }
        public int price { get; set; }
        public string status { get; set; }
        public int Quantity { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]

        public virtual User user { get; set; }

        [ForeignKey("Pet_Id")]

        public virtual Pet Pet { get; set; }
    }
}
