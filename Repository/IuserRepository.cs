using Pet_Shop_Management.Models;

namespace Pet_Shop_Management.Repository
{
    public interface IuserRepository
    {
        public bool Register(User user);
        public User GetUserByName(string name);
        public User GetUserByEmail(string email);
        public List<Pet> welcome();
        public string AddToCart(int Petid, int userId);
        public List<Cart> GetAllCartItems(int id);
        public bool AddQuantity(int id);
        public bool decreaseQuantity(int id);
        public List<Booking> GetAllBookings(int id);
        public List<Booking> GetAllBookingss(int userid);
        public bool deleteFormCart(int id);
    }
}
