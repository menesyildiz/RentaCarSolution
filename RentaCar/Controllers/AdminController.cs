using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentaCar.Entities;

namespace RentaCar.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private DatabaseContext _databaseContext;

        public AdminController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            List<Member> members = _databaseContext.Members.ToList();
            return View(members);
        }

        public IActionResult Confirm(int id)
        {
            Member member = _databaseContext.Members.Find(id);
            member.Confirmed = true;
            _databaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult MakeAdmin(int id)
        {
            Member member = _databaseContext.Members.Find(id);
            member.Role = "admin";
            _databaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Member member = _databaseContext.Members.Find(id);
            _databaseContext.Members.Remove(member);
            _databaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
