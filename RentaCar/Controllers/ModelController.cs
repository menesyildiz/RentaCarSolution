using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RentaCar.Entities;
using RentaCar.Models;

namespace RentaCar.Controllers
{
    public class ModelController : Controller
    {
        public IActionResult Index()
        {
            
            DatabaseContext db=new DatabaseContext();
            List<Modelx> modelxes = db.Models.Include(x => x.Brand).ToList();

            return View(modelxes);
        }

        public IActionResult CreateModelx()
        {
            DatabaseContext db = new DatabaseContext();
            List<Brand> brands = db.Brands.ToList();
            SelectList list = new SelectList(brands, "Id", "Name");
            NewModelxViewModel modelx= new NewModelxViewModel();
            modelx.Brands = list;

            return View(modelx);
        }

        [HttpPost]
        public IActionResult CreateModelx(NewModelxViewModel model)
        {
            DatabaseContext db = new DatabaseContext();

            if (ModelState.IsValid)
            {
                Modelx newModel = new Modelx();
                newModel.Name = model.Name;
                newModel.Details = model.Details;
                newModel.BrandId= model.BrandId;

                db.Models.Add(newModel);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            List<Brand> brands = db.Brands.ToList();
            SelectList list = new SelectList(brands, "Id", "Name");
            model.Brands = list;
            return View(model);
        }

        public IActionResult EditModel(int modelId)
        {
            DatabaseContext db = new DatabaseContext();
            Modelx modelx = db.Models.Find(modelId);
            List<Brand> brands = db.Brands.ToList();
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
            DatabaseContext db = new DatabaseContext();
            Modelx modelx = db.Models.Find(modelId);



            if (ModelState.IsValid)
            {
                modelx.Name = modelim.Name;
                modelx.Details = modelim.Details;
                modelx.BrandId= modelim.BrandId;
                

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            List<Brand> brands = db.Brands.ToList();
            SelectList list = new SelectList(brands, "Id", "Name");
            modelim.Brands = list;
            return View(modelim);

        }

        public IActionResult DeleteModel(int modelId)
        {
            DatabaseContext db = new DatabaseContext();
            Modelx modelx = db.Models.Find(modelId);
            db.Models.Remove(modelx);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       



    }
}
