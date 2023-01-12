using Microsoft.AspNetCore.Mvc;
using RentaCar.Entities;
using RentaCar.Models;

namespace RentaCar.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult BrandList()
        {
            List<Brand> brands = new List<Brand>();
            DatabaseContext db = new DatabaseContext();
            brands = db.Brands.ToList();

            return View(brands);
        }

        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBrand(NewBrandModel modelim)
        {
            DatabaseContext db = new DatabaseContext();

            if (db.Brands.Any(x=>x.Name==modelim.Name))
            {
                ModelState.AddModelError("Name", "Daha önce tanımlanan marka");
            }
            
            if (modelim.AgeLimit < 18)
            {
                ModelState.AddModelError("AgeLimit", "18'den büyük yaş giriniz");
            }

            //ModelState.AddModelError("", "bir hata oluştu"); genel hata verdirirsek.

            if (ModelState.IsValid)
            {
                
                Brand brand = new Brand();
                brand.Name = modelim.Name;
                brand.AgeLimit = modelim.AgeLimit;
                brand.Description = modelim.Description;


                db.Brands.Add(brand);
                db.SaveChanges();

                return RedirectToAction("BrandList");
            }
            return View(modelim);
           
        }

        public IActionResult EditBrand(int brandId)
        {
            DatabaseContext db = new DatabaseContext();
            Brand brand = db.Brands.Find(brandId);
            EditBrandViewModel edit = new EditBrandViewModel();
            edit.Name = brand.Name;
            edit.AgeLimit = brand.AgeLimit;
            edit.Description = brand.Description;
            


            return View(edit);
        }

        [HttpPost]
        public IActionResult EditBrand(int brandId, EditBrandViewModel modelim)
        {
            DatabaseContext db = new DatabaseContext();
            Brand brand = db.Brands.Find(brandId);

            if (modelim.AgeLimit < 18)
            {
                ModelState.AddModelError("AgeLimit", "18'den büyük yaş giriniz");
            }

            if (ModelState.IsValid)
            {
                brand.Name = modelim.Name;
                brand.AgeLimit = modelim.AgeLimit;
                brand.Description = modelim.Description;

                db.SaveChanges();
                return RedirectToAction("BrandList");
            }
            return View(modelim);





        }

        public IActionResult DeleteBrand(int brandId)
        {
            DatabaseContext db = new DatabaseContext();
            Brand brand = db.Brands.Find(brandId);
            DeleteBrandViewModel delete = new DeleteBrandViewModel();
            delete.brand = brand;
            return View(delete);
        }

        [HttpPost]
        public IActionResult DeleteBrand(int brandId, Brand modelim)
        {
            DatabaseContext db = new DatabaseContext();
            Brand brand = db.Brands.Find(brandId);
            db.Brands.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("BrandList");
        }


    }
}
