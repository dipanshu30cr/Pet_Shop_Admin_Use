using Microsoft.EntityFrameworkCore;
using Pet_Shop_Management.Models;

namespace Pet_Shop_Management.context
{
    public class Pet_Shop_DBContext : DbContext
    {

        public Pet_Shop_DBContext(DbContextOptions<Pet_Shop_DBContext> context) : base(context)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Login>? Login { get; set; }
        public DbSet<Pet> pets { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Booking> orders { get; set; }

    }
}
