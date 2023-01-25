using Microsoft.AspNetCore.Mvc;
using RentaCar.Entities;
using RentaCar.Managers;
using RentaCar.Models;
using System.Diagnostics;

namespace RentaCar.Controllers
{
    public class HomeController : Controller
    {
        private HomeManager _homeManager;

        public HomeController(DatabaseContext databaseContext)
        {
            _homeManager = new HomeManager(databaseContext);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewCarModel modelim)
        {
            if (ModelState.IsValid)
            {
                _homeManager.Create(modelim);
                return RedirectToAction("CarList");
            }
            return View(modelim);
        }

        public IActionResult Rent(int carId)
        {
            Car car = _homeManager.GetById(carId);
            RentViewModel rent = new RentViewModel();
            rent.Car = car;

            return View(rent);
        }
        [HttpPost]
        public IActionResult Rent(int carId, RentViewModel modelim)
        {
            Car car = _homeManager.GetById(carId);
            _homeManager.RentACar(modelim, car);
            return RedirectToAction("CarList");
        }

        public IActionResult Receive(int carId)
        {
            Car car = _homeManager.GetById(carId);
            return View(car);
        }

        [HttpPost]
        public IActionResult Receive(int carId, Car modelim)
        {
            Car car = _homeManager.GetById(carId);
            _homeManager.ReceiveCar(car);
            return RedirectToAction("CarList");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Edit(int carId)
        {
            Car car = _homeManager.GetById(carId);
            EditViewModel edit = new EditViewModel();
            edit.Brand = car.Brand;
            edit.Model = car.Model;
            edit.Year = car.Year;
            edit.GearBox = car.GearBox;
            edit.Fuel = car.Fuel;
            edit.Color = car.Color;
            edit.Price = car.Price;


            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(int carId, EditViewModel modelim)
        {
            Car car = _homeManager.GetById(carId);
            _homeManager.Update(car, modelim);

            return RedirectToAction("CarList");
        }

        public IActionResult Delete(int carId)
        {
            Car car = _homeManager.GetById(carId);
            DeleteViewModel delete = new DeleteViewModel();
            delete.car = car;

            return View(delete);
        }

        [HttpPost]
        public IActionResult Delete(int carId, Car modelim)
        {
            Car car = _homeManager.GetById(carId);
            _homeManager.Delete(car);

            return RedirectToAction("CarList");
        }


        public IActionResult CarList()
        {
            List<Car> cars = _homeManager.List();
            return View(cars);
        }

        [HttpPost]
        public IActionResult CarList(string order, string filter)
        {
            List<Car> cars = _homeManager.List(order, filter);
            return View(cars);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}