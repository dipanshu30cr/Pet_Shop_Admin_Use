using Microsoft.AspNetCore.Mvc;
using Pet_Shop_Management.Models;
using Pet_Shop_Management.ImgAdder;
using Pet_Shop_Management.Services;

namespace Pet_Shop_Management.Controllers
{
    public class AdminController : Controller
    {
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IAdminServices _adminServices;
        readonly IPhotoImageProvider _imageProvider;
        public string FilePath;
        public AdminController(IAdminServices adminServices, IWebHostEnvironment webHostEnvironment, IPhotoImageProvider imageProvider)
        {
            _adminServices = adminServices;
            _webHostEnvironment = webHostEnvironment;
            _imageProvider = imageProvider;
        }

        [HttpGet]
        public ActionResult AddNewPet()
        {
            string name = HttpContext.Session.GetString("Name");

            //if (string.IsNullOrEmpty(name))
            //{
            //    return RedirectToAction("UserLogin","UserRegister");
            //}
            //else
            //{

                if (ModelState.IsValid)
                {
                    string Folder = "Images";
                }
                return View();
          //  }

        }
        [HttpPost]
        public ActionResult AddNewPet(IFormFile file, Pet movie)
        {
           
                FilePath = _imageProvider.UploadFileAsync(file);
                movie.Pet_Image_Url = FilePath;
            Pet addedMovie = _adminServices.AddNewPet(movie);
                if (addedMovie != null)
                {
                    TempData["Added"] = "Pet is added sucessfully";
                }
                ModelState.Clear();
                return View();
            
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            
                string name = HttpContext.Session.GetString("Name");

                if (string.IsNullOrEmpty(name))
                {
                    return RedirectToAction("UserLogin", "UserRegister");
                }
                else
                {

                Pet EditPet = _adminServices.GetPetById(id);
                    return View(EditPet);
                }
            
        }
        [HttpPost]
        public ActionResult Edit(IFormFile file, int id, Pet Movie)
        {
            FilePath = _imageProvider.UploadFileAsync(file);


            Pet FormtblMovie = _adminServices.GetPetById(id);
            FormtblMovie.Pet_name = Movie.Pet_name;
            FormtblMovie.Pet_price = Movie.Pet_price;
            FormtblMovie.Pet_description = Movie.Pet_description;
            if (file != null)
            {
                FormtblMovie.Pet_Image_Url = FilePath;
            }






            bool EditStatus = _adminServices.Edit(FormtblMovie);
            return RedirectToAction("AdminDashboard");

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
                string name = HttpContext.Session.GetString("Name");

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("UserLogin", "UserRegister");
            }
            else
            {

                Pet MovieDetails = _adminServices.GetPetById(id);
                return View(MovieDetails);
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdminDashboard()
        {
            string adminName = HttpContext.Session.GetString("AdminName");
            ViewData["AdminDashboard"] = adminName;
            if (string.IsNullOrEmpty(adminName))
            {
                return RedirectToAction("UserLogin", "UserRegister");
            }
            TempData["Admin"] = adminName;
            List<Pet> Movis = _adminServices.AdminDashboard();
            return View(Movis);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            
                string name = HttpContext.Session.GetString("Name");

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("UserLogin", "UserRegister");
            }
            else
            {

                Pet DelteMovie = _adminServices.GetPetById(id);
                return View(DelteMovie);
            } 
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            int idx = int.Parse(id);
            Pet DelteMovie = _adminServices.GetPetById(idx);
            bool deleteStatus = _adminServices.Delete(DelteMovie);
            if (deleteStatus == true)
            {
                TempData["Deleted"] = "Pet is Deleted sucessfully";
            }
            return RedirectToAction("AdminDashboard");
        }

        public ActionResult Logout()
        {
            Console.WriteLine("before clearing Admin===" + HttpContext.Session.GetString("AdminName"));
            HttpContext.Session.SetString("AdminName", "");
            ModelState.Clear();
            Console.WriteLine("After clearing Admin ===" + HttpContext.Session.GetString("AdminName"));
            return RedirectToAction("AdminDashboard");
        }


       
    }
}
