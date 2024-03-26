using Pet_Shop_Management.Models;
using Pet_Shop_Management.Repository;

namespace Pet_Shop_Management.Services
{
    public class AdminServices : IAdminServices
    {
        readonly IAdminRepository _adminRepository;
        public AdminServices(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

      
        public bool Edit(Pet movie)
        {
            return _adminRepository.Edit(movie);
        }
        public bool Delete(Pet movie)
        {
            return _adminRepository.Delete(movie);
        }
        public Pet GetPetById(int id)
        {
            return _adminRepository.GetPetById(id);
        }
        public List<Pet> AdminDashboard()
        {
            return _adminRepository.AdminDashboard();
        }
        public Pet AddNewPet(Pet dish)
        {
            _adminRepository.AddNewPet(dish);
            return dish;
        }
    }
}
