using Microsoft.AspNetCore.Mvc;
using RentaCar.Entities;
using RentaCar.Managers;
using RentaCar.Models;

namespace RentaCar.Controllers
{
    public class BrandController : Controller
    {
        private BrandManager _brandManager;

        public BrandController(DatabaseContext databaseContext)
        {
            _brandManager = new BrandManager(databaseContext);
        }


        public IActionResult BrandList()
        {
            List<Brand> brands = _brandManager.List();
            return View(brands);
        }

        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBrand(NewBrandModel modelim)
        {
            if (_brandManager.IsNameExists(modelim.Name))
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
                _brandManager.Create(modelim);

                return RedirectToAction("BrandList");
            }
            return View(modelim);

        }

        public IActionResult EditBrand(int brandId)
        {
            Brand brand = _brandManager.GetById(brandId);
            EditBrandViewModel edit = new EditBrandViewModel();
            edit.Name = brand.Name;
            edit.AgeLimit = brand.AgeLimit;
            edit.Description = brand.Description;

            return View(edit);
        }

        [HttpPost]
        public IActionResult EditBrand(int brandId, EditBrandViewModel modelim)
        {
            Brand brand = _brandManager.GetById(brandId);

            if (modelim.AgeLimit < 18)
            {
                ModelState.AddModelError("AgeLimit", "18'den büyük yaş giriniz");
            }

            if (ModelState.IsValid)
            {
                _brandManager.Update(brand, modelim);

                return RedirectToAction("BrandList");
            }

            return View(modelim);
        }

        public IActionResult DeleteBrand(int brandId)
        {
            Brand brand = _brandManager.GetById(brandId);
            DeleteBrandViewModel delete = new DeleteBrandViewModel();
            delete.brand = brand;

            return View(delete);
        }

        [HttpPost]
        public IActionResult DeleteBrand(int brandId, Brand modelim)
        {
            Brand brand = _brandManager.GetById(brandId);
            _brandManager.Delete(brand);

            return RedirectToAction("BrandList");
        }


    }
}
