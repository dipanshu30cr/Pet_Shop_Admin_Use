using Microsoft.EntityFrameworkCore;
using Pet_Shop_Management.context;
using Pet_Shop_Management.Models;
using System.Collections.Generic;

namespace Pet_Shop_Management.Repository
{
    public class AdminRepository: IAdminRepository
    {
        Pet_Shop_DBContext _Pet_Shop_DBContext;
        public AdminRepository(Pet_Shop_DBContext MovieAppDBContext)
        {
            _Pet_Shop_DBContext = MovieAppDBContext;
        }
        public Pet AddNewPet(Pet pet)
        { /*string adminId = HttpContext.Session.GetString("AdminId");*/
            pet.Pet_created_date = DateTime.Now;
            pet.IsDeleted = false;
            pet.created_By_Id = 1;


            _Pet_Shop_DBContext.pets.Add(pet);
            _Pet_Shop_DBContext.SaveChanges();
            return pet;

        }
        public bool Edit(Pet pet)
        {
            _Pet_Shop_DBContext.Entry<Pet>(pet).State = EntityState.Modified;
            _Pet_Shop_DBContext.SaveChanges();
            return true;
        }
        public bool Delete(Pet pet)
        {
            pet.IsDeleted = true;
            _Pet_Shop_DBContext.Entry<Pet>(pet).State = EntityState.Modified;
            _Pet_Shop_DBContext.SaveChanges();
            return true;
        }
        public Pet GetPetById(int id)
        {
            return _Pet_Shop_DBContext.pets.Where(D => D.Pet_Id == id).FirstOrDefault();
        }
        public List<Pet> AdminDashboard()
        {
            List<Pet> movies = _Pet_Shop_DBContext.pets.Where(D => D.IsDeleted == false).ToList();
            return movies;
        }
       
    }
}
