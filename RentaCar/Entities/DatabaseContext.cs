using Microsoft.EntityFrameworkCore;

namespace RentaCar.Entities
{
    public class DatabaseContext : DbContext // dbcontextten miras aldık 
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Modelx> Models { get; set; }


    }
}
