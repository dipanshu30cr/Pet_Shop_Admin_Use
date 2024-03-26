using Pet_Shop_Management.Models;
using Pet_Shop_Management.Repository;
using System.Web.Helpers;

namespace Pet_Shop_Management.Services
{
    public class UserServices:IuserServices
    {
        readonly IuserRepository _userRepository;
        private object _Pet_Shop_DBContext;

        public UserServices(IuserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetUserByName(string name)
        {

            User userr = _userRepository.GetUserByName(name);
            Console.WriteLine(userr);
            return userr;
        }
        public User Login(string email, string password)
        {
            User user = _userRepository.GetUserByEmail(email);

            if (Crypto.VerifyHashedPassword(user.Password, password))
            {
                return user;
            }
            else
            {
                return user;
            }



        }
        public bool deleteFormCart(int id)
        {
            return _userRepository.deleteFormCart(id);
        }
        public List<Booking> GetAllBookings(int id)
        {
            return _userRepository.GetAllBookings(id);
        }
        public bool decreaseQuantity(int id)
        {
            return _userRepository.decreaseQuantity(id);
        }
        public bool AddQuantity(int id)
        {
            return _userRepository.AddQuantity(id);
        }
        public List<Cart> GetAllCartItems(int id)
        {
            return _userRepository.GetAllCartItems(id);
        }
        public List<Booking> GetAllBookingss(int userid)
        {
            return _userRepository.GetAllBookingss(userid);
        }
        public string AddToCart(int dishId, int userId)
        {
            string status = _userRepository.AddToCart(dishId, userId);
            return status;
        }
        public List<Pet> welcome()
        {
            return _userRepository.welcome();
        }
        public User GetUserByEmail(string email)
        {

            User userr = _userRepository.GetUserByEmail(email);
            Console.WriteLine(userr);
            return userr;
        }


        public string Register(User user)
        {
            var NameExists = _userRepository.GetUserByName(user.Name);
            var EmailExists = _userRepository.GetUserByEmail(user.Email);
            if (NameExists != null)
            {
                return "Name";
            }
            else if (EmailExists != null)
            {
                return "Email";
            }
            if (NameExists == null && EmailExists == null)
            {
                _userRepository.Register(user);
                return "Pass";
            }
            else
            {
                return " both";
            }

        }
    }
}
