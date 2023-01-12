using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RentaCar.Entities
{
    public class DatabaseContext:DbContext // dbcontextten miras aldık 
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Modelx> Models { get; set; }

        public DatabaseContext()
        {
            //Database.EnsureCreated(); //database'in oluşmasını garantiliyoruz.

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; database=RentaCarDb; trusted_connection=true");
        }

    }
}
