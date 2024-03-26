using Pet_Shop_Management.Models;

namespace Pet_Shop_Management.Services
{
    public interface IuserServices
    {
        string Register(User user);
        public User GetUserByName(string name);
        public User GetUserByEmail(string email);
        public User Login(string email, string password);
        public List<Pet> welcome();
        public string AddToCart(int Petid, int userId);
        public List<Cart> GetAllCartItems(int id);
        public bool AddQuantity(int id);
        public bool decreaseQuantity(int id);
        public List<Booking> GetAllBookings(int id);
        public bool deleteFormCart(int id);
    }
}
