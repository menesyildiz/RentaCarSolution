using Microsoft.AspNetCore.Mvc;
using RentaCar.Entities;
using RentaCar.Models;

namespace RentaCar.Controllers
{
    public class CustomerController:Controller
    {
        public IActionResult CustomerList()
        {
            List<Customer> customers = new List<Customer>();
            DatabaseContext db = new DatabaseContext();
            customers = db.Customers.ToList();

            return View(customers);
        }

        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(NewCustomerModel modelim)
        {
            DatabaseContext db = new DatabaseContext();

            //if (db.Brands.Any(x => x.Name == modelim.Name))
            //{
            //    ModelState.AddModelError("Name", "Daha önce tanımlanan marka");
            //}

            //if (modelim.AgeLimit < 18)
            //{
            //    ModelState.AddModelError("AgeLimit", "18'den büyük yaş giriniz");
            //}

            //ModelState.AddModelError("", "bir hata oluştu"); genel hata verdirirsek.
            Customer customer = new Customer();
            
            if (ModelState.IsValid)
            {

                customer.IdNumber = modelim.IdNumber;
                customer.Name= modelim.Name;
                customer.Surname= modelim.Surname;
                customer.Birthday= modelim.Birthday;
                customer.PhoneNumber= modelim.PhoneNumber;
                customer.Email= modelim.Email;


                db.Customers.Add(customer);
                db.SaveChanges();

                return RedirectToAction("CustomerList");
            }
            

            return View(modelim);

        }

        public IActionResult EditCustomer(int customerId)
        {
            DatabaseContext db = new DatabaseContext();
            Customer customer=db.Customers.Find(customerId);
            
            EditCustomerViewModel edit = new EditCustomerViewModel();
           
            edit.IdNumber = customer.IdNumber;
            edit.Name= customer.Name;
            edit.Surname = customer.Surname;
            edit.Birthday= customer.Birthday;
            edit.PhoneNumber = customer.PhoneNumber;
            edit.Email= customer.Email;


            return View(edit);
        }

        [HttpPost]
        public IActionResult EditCustomer(int customerId, EditCustomerViewModel modelim)
        {
            DatabaseContext db = new DatabaseContext();
            Customer customer = db.Customers.Find(customerId);

            

            if (ModelState.IsValid)
            {
                customer.IdNumber = modelim.IdNumber;
                customer.Name = modelim.Name;
                customer.Surname = modelim.Surname;
                customer.Birthday = modelim.Birthday;
                customer.PhoneNumber = modelim.PhoneNumber;
                customer.Email = modelim.Email;

                db.SaveChanges();

                return RedirectToAction("CustomerList");
            }
            return View(modelim);
            
        }



        public IActionResult DeleteCustomer(int customerId)
        {
            DatabaseContext db = new DatabaseContext();
            Customer customer = db.Customers.Find(customerId);
            DeleteCustomerViewModel delete = new DeleteCustomerViewModel();
            delete.customer = customer;

            return View(delete);
        }

        [HttpPost]
        public IActionResult DeleteCustomer(int customerId, Customer modelim)
        {
            DatabaseContext db = new DatabaseContext();
            Customer customer = db.Customers.Find(customerId);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("CustomerList");
        }

    }
}
