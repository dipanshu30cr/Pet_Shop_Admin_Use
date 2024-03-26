using Pet_Shop_Management.Models;

namespace Pet_Shop_Management.Repository
{
    public interface IAdminRepository
    {
       
        public Pet AddNewPet(Pet pet);
        public List<Pet> AdminDashboard();
        public Pet GetPetById(int id);
        public bool Delete(Pet pet);
        public bool Edit(Pet pet);
    }
}
