using Microsoft.AspNetCore.Mvc;
using RentaCar.Entities;
using RentaCar.Managers;
using RentaCar.Models;

namespace RentaCar.Controllers
{
    public class CustomerController:Controller
    {
        private CustomerManager _customerManager;
        public IActionResult CustomerList()
        {
            List<Customer> customers = _customerManager.List();
            return View(customers);
        }

        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(NewCustomerModel modelim)
        {
            List<Customer> customers = _customerManager.List();

            //if (db.Brands.Any(x => x.Name == modelim.Name))
            //{
            //    ModelState.AddModelError("Name", "Daha önce tanımlanan marka");
            //}

            //if (modelim.AgeLimit < 18)
            //{
            //    ModelState.AddModelError("AgeLimit", "18'den büyük yaş giriniz");
            //}

            //ModelState.AddModelError("", "bir hata oluştu"); genel hata verdirirsek.


            if (ModelState.IsValid)
            {

                _customerManager.Create(modelim);

                return RedirectToAction("CustomerList");
            }
            

            return View(modelim);

        }

        public IActionResult EditCustomer(int customerId)
        {
            Customer customer = _customerManager.GetById(customerId);
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
            Customer customer = _customerManager.GetById(customerId); 

            if (ModelState.IsValid)
            {
                _customerManager.Update(customer, modelim);

                return RedirectToAction("CustomerList");
            }
            return View(modelim);
            
        }



        public IActionResult DeleteCustomer(int customerId)
        {
            Customer customer = _customerManager.GetById(customerId);
            DeleteCustomerViewModel delete = new DeleteCustomerViewModel();
            delete.customer = customer;

            return View(delete);
        }

        [HttpPost]
        public IActionResult DeleteCustomer(int customerId, Customer modelim)
        {
            Customer customer = _customerManager.GetById(customerId);
            _customerManager.Delete(customer);
            return RedirectToAction("CustomerList");
        }

    }
}
