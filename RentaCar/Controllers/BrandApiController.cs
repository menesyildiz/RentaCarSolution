using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentaCar.Entities;
using RentaCar.Managers;
using RentaCar.Models;

namespace RentaCar.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandApiController : ControllerBase
    {
        private IBrandManager _brandManager;

        public BrandApiController(IBrandManager brandManager)
        {
            _brandManager = brandManager;
        }

        [HttpGet]
        public List<Brand> GetAllBrands()
        {
            return _brandManager.List();
        }

        [HttpPost]
        public List<Brand> AddNewBrand(NewBrandModel model)
        {
            _brandManager.Create(model);
            return _brandManager.List();
        }
    }
}
