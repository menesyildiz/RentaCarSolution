using RentaCar.Entities;
using RentaCar.Models;

namespace RentaCar.Managers
{
    public class BrandManager
    {
        private DatabaseContext _databaseContext;

        public BrandManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Brand> List()
        {
            return _databaseContext.Brands.ToList();
        }

        public bool IsNameExists(string name)
        {
            return _databaseContext.Brands.Any(x => x.Name == name);
        }

        public void Create(NewBrandModel modelim)
        {
            Brand brand = new Brand();
            brand.Name = modelim.Name;
            brand.AgeLimit = modelim.AgeLimit;
            brand.Description = modelim.Description;


            _databaseContext.Brands.Add(brand);
            _databaseContext.SaveChanges();
        }

        public Brand GetById(int brandId)
        {
            return _databaseContext.Brands.Find(brandId);
        }

        public void Update(Brand brand, EditBrandViewModel modelim)
        {
            brand.Name = modelim.Name;
            brand.AgeLimit = modelim.AgeLimit;
            brand.Description = modelim.Description;

            _databaseContext.SaveChanges();
        }

        public void Delete(Brand brand)
        {
            _databaseContext.Brands.Remove(brand);
            _databaseContext.SaveChanges();
        }
    }
}
