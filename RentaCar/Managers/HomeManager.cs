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
        
        public void Create(NewCarModel modelim)
        {
            Car car = new Car();
            car.Brand = modelim.Brand;
            car.Model = modelim.Model;
            car.Year = modelim.Year;
            car.GearBox = modelim.GearBox;
            car.Fuel = modelim.Fuel;
            car.Color = modelim.Color;
            car.Price = modelim.Price;
            car.Rented = false;

            _databaseContext.Cars.Add(car);
            _databaseContext.SaveChanges();
        }

        public void RentACar(RentViewModel modelim, Car car)
        {
            car.RentDate = DateTime.Now;
            car.DeliveryDate = modelim.DeliveryDate;
            car.Rented = true;

            _databaseContext.SaveChanges();
        }

        public void ReceiveCar(Car car)
        {
            car.RentDate = null;
            car.DeliveryDate = null;
            car.Rented = false;

            _databaseContext.SaveChanges();
        }

        public List<Car> List()
        {
            return _databaseContext.Cars.ToList();
        }

        public List<Car> List(string order, string filter)
        {
            List<Car> cars = List();

            if (order == "IP")
            {
                return cars.OrderBy(x => x.Price).ToList();
            }
            else if (order == "DP")
            {
                return cars.OrderByDescending(x => x.Price).ToList();
            }
            else if (order == "ASC")
            {
                return cars.OrderBy(x => x.Brand).ToList();
            }
            else if (order == "DESC")
            {
                return cars.OrderByDescending(x => x.Brand).ToList();
            }
            else if (filter == "Rented")
            {
                return cars.Where(x => x.Rented == true).ToList();
            }
            else if (filter == "NonRented")
            {
                return cars.Where(x => x.Rented == false).ToList();
            }
            else if (filter == "GearBoxManu")
            {
                return cars.Where(x => x.GearBox == "Manuel").ToList();
            }
            else if (filter == "GearBoxAuto")
            {
                return cars.Where(x => x.GearBox == "Automatic").ToList();
            }
            else
            {
                return cars;
            }
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
