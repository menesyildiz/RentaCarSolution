﻿using Microsoft.AspNetCore.Mvc;
using RentaCar.Entities;
using RentaCar.Models;
using System.Diagnostics;

namespace RentaCar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewCarModel modelim)
        {
            DatabaseContext db= new DatabaseContext();

            if (ModelState.IsValid)
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

                db.Cars.Add(car);
                db.SaveChanges();

                return RedirectToAction("CarList");
            }
            return View(modelim);
        }

        public IActionResult Rent(int carId)
        {
            DatabaseContext db=new DatabaseContext();
            Car car = db.Cars.Find(carId);
            RentViewModel rent = new RentViewModel();
            rent.Car = car;

            return View(rent);
        }
        [HttpPost]
        public IActionResult Rent(int carId, RentViewModel modelim)
        {
            DatabaseContext db = new DatabaseContext();
            Car car =db.Cars.Find(carId);
            car.RentDate = DateTime.Now;
            car.DeliveryDate = modelim.DeliveryDate;
            car.Rented = true;

            db.SaveChanges();
            return RedirectToAction("CarList");
        }

        public IActionResult Receive(int carId)
        {
            DatabaseContext db = new DatabaseContext();
            Car car = db.Cars.Find(carId);
            

           
            return View(car);
        }

        [HttpPost]
        public IActionResult Receive(int carId, Car modelim)
        {
            DatabaseContext db = new DatabaseContext();
            Car car = db.Cars.Find(carId);
            car.RentDate = null;
            car.DeliveryDate = null;
            car.Rented = false;

            db.SaveChanges();
            return RedirectToAction("CarList");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Edit(int carId)
        {
            DatabaseContext db = new DatabaseContext();
            Car car = db.Cars.Find(carId);
            EditViewModel edit = new EditViewModel();
            edit.Brand = car.Brand;
            edit.Model = car.Model;
            edit.Year= car.Year;
            edit.GearBox= car.GearBox;
            edit.Fuel= car.Fuel;
            edit.Color = car.Color;
            edit.Price= car.Price;
            

            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(int carId, EditViewModel modelim)
        {
            DatabaseContext db = new DatabaseContext();
            Car car = db.Cars.Find(carId);
            car.Brand = modelim.Brand;
            car.Model= modelim.Model;
            car.Year= modelim.Year;
            car.GearBox=modelim.GearBox;
            car.Fuel= modelim.Fuel;
            car.Color= modelim.Color;
            car.Price= modelim.Price;

            db.SaveChanges();
            return RedirectToAction("CarList");
        }

        public IActionResult Delete(int carId)
        {
            DatabaseContext db = new DatabaseContext();
            Car car = db.Cars.Find(carId);
            DeleteViewModel delete=new DeleteViewModel();
            delete.car = car;
            return View(delete);
        }

        [HttpPost]
        public IActionResult Delete(int carId, Car modelim)
        {
            DatabaseContext db = new DatabaseContext();
            Car car = db.Cars.Find(carId);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("CarList");
        }


        public IActionResult CarList()
        {
            List<Car> cars= new List<Car>();
            DatabaseContext db = new DatabaseContext();
            cars = db.Cars.ToList();

            return View(cars);
        }

        [HttpPost]
        public IActionResult CarList(string order, string filter)
        {
            List<Car> cars = new List<Car>();
            DatabaseContext db = new DatabaseContext();

            if (order=="IP")
            {   
              cars = db.Cars.OrderBy(x=>x.Price).ToList();
            }
            else if(order=="DP")
            {
                cars = db.Cars.OrderByDescending(x => x.Price).ToList();
            }
            else if (order=="ASC")
            {
                cars = db.Cars.OrderBy(x => x.Brand).ToList();
            }
            else if(order=="DESC")
            {
                cars = db.Cars.OrderByDescending(x => x.Brand).ToList();
            }
            else if (filter == "Rented")
            {
                cars = db.Cars.Where(x=>x.Rented==true).ToList();
            }
            else if (filter == "NonRented")
            {
                cars = db.Cars.Where(x => x.Rented == false).ToList();
            }
            else if (filter == "GearBoxManu")
            {
                cars = db.Cars.Where(x => x.GearBox == "Manuel").ToList();
            }
            else if (filter == "GearBoxAuto")
            {
                cars = db.Cars.Where(x => x.GearBox == "Automatic").ToList();
            }
            else
            {
                cars = db.Cars.ToList();
            }

            
            return View(cars);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}