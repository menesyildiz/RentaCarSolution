using Microsoft.EntityFrameworkCore;
using RentaCar.Entities;
using RentaCar.Models;

namespace RentaCar.Managers
{
    public class ModelManager
    {
        private DatabaseContext _databaseContext;

        public ModelManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Modelx> List()
        {
            return _databaseContext.Models.Include(x => x.Brand).ToList();
        }

        public void Create(NewModelxViewModel modelim)
        {
            Modelx newModel = new Modelx();
            newModel.Name = modelim.Name;
            newModel.Details = modelim.Details;
            newModel.BrandId = modelim.BrandId;

            _databaseContext.Models.Add(newModel);
            _databaseContext.SaveChanges();
        }

        public Modelx GetById(int modelId)
        {
            return _databaseContext.Models.Find(modelId);
        }

        public void Update(Modelx modelx, EditModelxViewModel modelim)
        {
            modelx.Name = modelim.Name;
            modelx.Details = modelim.Details;
            modelx.BrandId = modelim.BrandId;

            _databaseContext.SaveChanges();
        }

        public void Delete(Modelx modelx)
        {
            _databaseContext.Models.Remove(modelx);
            _databaseContext.SaveChanges();
        }
    }
}
