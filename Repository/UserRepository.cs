using Microsoft.EntityFrameworkCore;
using Pet_Shop_Management.context;
using Pet_Shop_Management.Models;
using System.Web.Helpers;

namespace Pet_Shop_Management.Repository
{
    public class UserRepository: IuserRepository
    {
        Pet_Shop_DBContext _Pet_Shop_DBContext;
        public UserRepository(Pet_Shop_DBContext Pet_Shop_DBContext)
        {
            _Pet_Shop_DBContext = Pet_Shop_DBContext;
        }

        public List<Pet> welcome()
        {
            return _Pet_Shop_DBContext.pets.Where(X => X.IsDeleted == false).ToList();
        }
        public bool AddQuantity(int id)
        {
            Cart cart = _Pet_Shop_DBContext.carts.Where(x => x.Cart_Id == id).FirstOrDefault();

            cart.Quantity = cart.Quantity + 1;

            _Pet_Shop_DBContext.Entry<Cart>(cart).State = EntityState.Modified;
            _Pet_Shop_DBContext.SaveChanges();
            return true;


        }
        public List<Booking> GetAllBookings(int id)
        {
            List<Booking> booking = _Pet_Shop_DBContext.orders.Where(x => x.UserId == id).Include(x => x.Pet).ToList();
            return booking;
        }

        public bool deleteFormCart(int id)
        {
            Cart cart = _Pet_Shop_DBContext.carts.Where(x => x.Cart_Id == id).FirstOrDefault();
            _Pet_Shop_DBContext.Remove(cart);
            _Pet_Shop_DBContext.SaveChanges();
            return true;
        }
        public bool decreaseQuantity(int id)
        {
            Cart cart = _Pet_Shop_DBContext.carts.Where(x => x.Cart_Id == id).FirstOrDefault();
            if (cart.Quantity > 1)
            {
                cart.Quantity = cart.Quantity - 1;
            }
            else
            {
                cart.Quantity = cart.Quantity;
            }



            _Pet_Shop_DBContext.Entry<Cart>(cart).State = EntityState.Modified;
            _Pet_Shop_DBContext.SaveChanges();
            return true;


        }
        public bool Register(User user)
        {
            //    User use=new User{ Name=user.Name,
            //  Email=user.Email,
            //  Address=user.Address, 
            //  Password=Crypto.HashPassword(user.Password),
            //};
            var password = user.Password;
            user.Password = Crypto.HashPassword(password);
            user.Role = "User";
            _Pet_Shop_DBContext.users.Add(user);
            return _Pet_Shop_DBContext.SaveChanges() == 1 ? true : false;

        }

        public List<Booking> GetAllBookingss(int userid)
        {
            var orderItems = _Pet_Shop_DBContext.orders.Where(item => item.UserId == userid).Include(x => x.Pet_Id).Include(x => x.user).ToList();
            return orderItems;
        }

        public string AddToCart(int Movieid, int userId)
        {
            Pet dish = _Pet_Shop_DBContext.pets.Where(x => x.Pet_Id == Movieid).FirstOrDefault();
            Cart crt = _Pet_Shop_DBContext.carts.Where(x => x.Pet_Id == Movieid).FirstOrDefault();

            if (crt == null)
            {
                Cart cart = new Cart();
                cart.User_Id = userId;
                cart.Pet_Id = Movieid;
                cart.Quantity = 1;
                _Pet_Shop_DBContext.carts.Add(cart);
                _Pet_Shop_DBContext.SaveChanges();
                return dish.Pet_name;

            }
            else
            {
                Cart cart = _Pet_Shop_DBContext.carts.Where(x => x.Cart_Id == crt.Cart_Id).FirstOrDefault();

                cart.Quantity = cart.Quantity + 1;

                _Pet_Shop_DBContext.Entry<Cart>(cart).State = EntityState.Modified;
                _Pet_Shop_DBContext.SaveChanges();
                return dish.Pet_name;

            }



        }
        public List<Cart> GetAllCartItems(int id)
        {
            var cartItems = _Pet_Shop_DBContext.carts.Where(item => item.user.Id == id).Include(x => x.Pet).ToList();
            return cartItems;
        }


        public User GetUserByName(string name)
        {
            return _Pet_Shop_DBContext.users.Where(x => x.Name == name).FirstOrDefault();
        }
        public User GetUserByEmail(string email)
        {
            return _Pet_Shop_DBContext.users.Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
