using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RentaCar.Entities;
using RentaCar.Managers;
using RentaCar.Models;

namespace RentaCar.Controllers
{
    public class ModelController : Controller
    {
        private BrandManager _brandManager;
        private ModelManager _modelManager;
        public IActionResult Index()
        {

            List<Modelx> modelxes = _modelManager.List();

            return View(modelxes);
        }

        public IActionResult CreateModelx()
        {
            List<Brand> brands = _brandManager.List();
            List<Modelx> modelxes = _modelManager.List();
            SelectList list = new SelectList(brands, "Id", "Name");
            NewModelxViewModel modelx= new NewModelxViewModel();
            modelx.Brands = list;

            return View(modelx);
        }

        [HttpPost]
        public IActionResult CreateModelx(NewModelxViewModel model)
        {
            
            List<Modelx> modelxes = _modelManager.List();

            if (ModelState.IsValid)
            {
                _modelManager.Create(model);

                return RedirectToAction("Index");

            }
            List<Brand> brands = _brandManager.List();
            SelectList list = new SelectList(brands, "Id", "Name");
            model.Brands = list;
            return View(model);
        }

        public IActionResult EditModel(int modelId)
        {
            Modelx modelx = _modelManager.GetById(modelId);
            List<Brand> brands = _brandManager.List();
            SelectList list = new SelectList(brands, "Id", "Name");

            EditModelxViewModel edit = new EditModelxViewModel();
            edit.Brands = list;
            edit.Name = modelx.Name;
            edit.Details = modelx.Details;
            edit.BrandId = modelx.BrandId;
        
            return View(edit);
        }

        [HttpPost]
        public IActionResult EditModel(int modelId, EditModelxViewModel modelim)
        {
            Modelx modelx = _modelManager.GetById(modelId);

            if (ModelState.IsValid)
            {
                _modelManager.Update(modelx, modelim);

                return RedirectToAction("Index");
            }
            List<Brand> brands = _brandManager.List();
            SelectList list = new SelectList(brands, "Id", "Name");
            modelim.Brands = list;
            return View(modelim);

        }

        public IActionResult DeleteModel(int modelId)
        {
            Modelx modelx = _modelManager.GetById(modelId);
            _modelManager.Delete(modelx);
            return RedirectToAction("Index");
        }

       //eretert



    }
}
