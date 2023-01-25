using RentaCar.Entities;
using RentaCar.Models;
using System.Linq;

namespace RentaCar.Managers
{
    public class HomeManager
    {
        private DatabaseContext _databaseContext;

        public HomeManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public List<Car> List()
        {
            return _databaseContext.Cars.ToList();
        }

        public List<Car> SortIncresingPrice(string order, string filter)
        {
            return _databaseContext.Cars.OrderBy(x=>x.Price).ToList();
        }

        public Car GetById(int carId)
        {
            return _databaseContext.Cars.Find(carId);
        }

        public void Update(Car car, EditViewModel modelim)
        {
            car.Brand = modelim.Brand;
            car.Model = modelim.Model;
            car.Year = modelim.Year;
            car.GearBox = modelim.GearBox;
            car.Fuel = modelim.Fuel;
            car.Color = modelim.Color;
            car.Price = modelim.Price;

            _databaseContext.SaveChanges();
        }

        public void Delete(Car car)
        {
            _databaseContext.Cars.Remove(car);
            _databaseContext.SaveChanges();
        }
    }
}
