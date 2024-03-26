using IronPdf;
using Microsoft.AspNetCore.Mvc;
using Pet_Shop_Management.context;
using Pet_Shop_Management.Models;
using System.Web.Helpers;
using Pet_Shop_Management.Services;
using Pet_Shop_Management.Models;
using Pet_Shop_Management.Services;

namespace Pet_Shop_Management.Controllers
{
    public class UserRegisterController : Controller
    {
        readonly IuserServices _userServices;
        readonly Pet_Shop_DBContext _pet_Shop_DBContext;
        public UserRegisterController(IuserServices userServices, Pet_Shop_DBContext pet_Shop_DBContext)
        {
            _userServices = userServices;
            _pet_Shop_DBContext = pet_Shop_DBContext;
        }
        public ActionResult welcome()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["UserDashBoard"] = name;
            if (string.IsNullOrEmpty(name))
            {

                return RedirectToAction("UserLogin");
            }
            else
            {
                TempData["Name"] = name;
                List<Pet> movies = _userServices.welcome();

                return View(movies);
            }
        }

        public ActionResult Logout()
        {

            HttpContext.Session.SetString("Name", "");

            return RedirectToAction("welcome");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            string addStatus = _userServices.Register(user);


            if (addStatus == "Pass")
            {
                TempData["AddMsg"] = "Sucessfully Registered";
                TempData["Pass"] = "Pass";
                ModelState.Clear();
                return View();
            }
            else if (addStatus == "Name")
            {
                TempData["AddMsg"] = "Name you want To Add Already Exists";
                TempData["FailName"] = "FailN";

            }
            else if (addStatus == "Email")
            {
                TempData["AddMsg"] = "Email you want To Add Already Exists";
                TempData["FailEmail"] = "FailE";

            }
            else if (addStatus == "both")
            {
                TempData["AddMsg"] = "Name and Email you want To Add Already Exists";
                TempData["FailBoth"] = "FailB";

            }

            return View();

        }
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(Login loginInfo)
        {
            //throw new Exception("this is new Exception");
            User user = _userServices.Login(loginInfo.Email, loginInfo.Password);
            if (Crypto.VerifyHashedPassword(user.Password, loginInfo.Password))
            {

                if (user.Role == "User")
                {

                    HttpContext.Session.SetString("Name", user.Name);
                    HttpContext.Session.SetString("Id", user.Id.ToString());
                    return RedirectToAction("welcome");

                }
                else if (user.Role == "Admin")
                {

                    HttpContext.Session.SetString("AdminName", user.Name);
                    HttpContext.Session.SetString("AdminId", user.Id.ToString());
                    return RedirectToAction("AdminDashboard", "Admin");
                }

                return View();
            }
            else
            {
                TempData["AddMsgLogin"] = "Incorect Email or password ";
                TempData["FailLogin"] = "Fail";
                return View();
            }

        }
        [HttpGet]
        public ActionResult Bill()
        {
            string name = HttpContext.Session.GetString("Name");

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("UserLogin");
            }
            else
            {
                User user = _userServices.GetUserByName(name);
                string invoice = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("invoice", invoice);
                List<Cart> cartitems = _userServices.GetAllCartItems(user.Id).ToList();
                TempData["Invoice"] = invoice;

                return View(cartitems);
            }



        }

        [HttpGet]
        public ActionResult Checkout()
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["UserDashBoard"] = name;
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("UserLogin");
            }
            else
            {



                User user = _userServices.GetUserByName(name);

                string invoice = HttpContext.Session.GetString("invoice");
                List<Cart> cartitems = _userServices.GetAllCartItems(user.Id).ToList();
                List<Booking> booking = new List<Booking>();
                cartitems.ForEach(x =>
                {
                    booking.Add(new Booking()
                    {
                        Pet_Id = x.Pet_Id,
                        UserId = x.User_Id,
                        Invoice = invoice,
                        price = x.Pet.Pet_price * x.Quantity,
                        BookingDate = DateTime.Now,
                        status = "Booked",
                        Quantity = x.Quantity

                    });

                });
                _pet_Shop_DBContext.orders.AddRange(booking);
                _pet_Shop_DBContext.SaveChanges();

                _pet_Shop_DBContext.carts.RemoveRange(cartitems);
                _pet_Shop_DBContext.SaveChanges();
                HttpContext.Session.SetString("invoice", "");
                TempData["msg"] = "Payment sucessful.. Booking Placed";
                return RedirectToAction("GetAllCartItems");
            }

        }
        public ActionResult deleteFormCart(int id)

        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["UserDashBoard"] = name;
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("UserLogin");
            }
            else
            {
                _userServices.deleteFormCart(id);
                TempData["msg"] = "Sucessfully Deleted the Item";
                return RedirectToAction("GetAllCartItems");
            }
        }

        [HttpPost]
        public JsonResult UserExistsName(string Name)
        {
            Console.WriteLine("inside remote method");
            var user = _userServices.GetUserByName(Name);

            return Json(user == null);
        }
        [HttpGet]
        public ActionResult GetAllCartItems()
        {


            string name = HttpContext.Session.GetString("Name");
            ViewData["UserDashBoard"] = name;

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("UserLogin");
            }
            else
            {
                User user = _userServices.GetUserByName(name);
                List<Cart> cart = _userServices.GetAllCartItems(user.Id);


                return View(cart);

            }


        }
        public ActionResult AddQuantity(int id)
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["UserDashBoard"] = name;

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("UserLogin");
            }
            else
            {
                _userServices.AddQuantity(id);
                return RedirectToAction("GetAllCartItems");

            }

        }
        public ActionResult decreaseQuantity(int id)
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["UserDashBoard"] = name;

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("UserLogin");
            }
            else
            {
                _userServices.decreaseQuantity(id);
                return RedirectToAction("GetAllCartItems");
            }

        }


        [HttpGet]
        public ActionResult GetAllBookings()
        {
            string name = HttpContext.Session.GetString("Name");


            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("UserLogin");
            }
            else
            {
                User user = _userServices.GetUserByName(name);
                List<Booking> orders = _userServices.GetAllBookings(user.Id);
                return View(orders);
            }

        }
        [HttpGet]
        public ActionResult AddToCart(int Id)
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["UserDashBoard"] = name;

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("UserLogin");
            }
            else
            {
                Cart crt = _pet_Shop_DBContext.carts.Where(x => x.Cart_Id == Id).FirstOrDefault();
                var user = _userServices.GetUserByName(name);

                string status = _userServices.AddToCart(Id, user.Id);

                TempData["msg"] = $"Sucessfully Added {status} to Cart";
                return RedirectToAction("welcome");
            }

        }
        public ActionResult GetAllBookingss(int userid)
        {
            string name = HttpContext.Session.GetString("Name");
            ViewData["UserDashBoard"] = name;
            var user = _userServices.GetUserByName(name);


            var olist = _userServices.GetAllBookings(user.Id).DistinctBy(item => item.Invoice);
            return View(olist);
        }
        public IActionResult ViewOrderDetails(string id)
        {

            string name = HttpContext.Session.GetString("Name");
            ViewData["UserDashBoard"] = name;
            var user = _userServices.GetUserByName(name);

            var olist = _userServices.GetAllBookings(user.Id).Where(item => item.Invoice == id).ToList();
            User u = olist.First().user;
            ViewBag.Address = u.Address;
            ViewBag.Invoice = id;
            double orderTotal = 0;
            olist.ForEach(o =>
            {
                orderTotal += o.Quantity * o.Pet.Pet_price;
            });
            ViewBag.OrderTotal = orderTotal;
            return View(olist);
        }


        [HttpPost]
        public JsonResult UserExistsEmail(string Email)
        {
            Console.WriteLine("inside remote method");
            var user = _userServices.GetUserByEmail(Email);

            return Json(user == null);
          }


    }
}
