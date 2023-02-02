using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RentaCar.Entities;
using RentaCar.Managers;
using RentaCar.Models;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using System.Xml.Linq;

namespace RentaCar.Controllers
{
    public class AccountController : Controller
    {
        private IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool check = _accountManager.CheckMember(model);

                if (check == true)
                {
                    Member member = _accountManager.GetMember(model.Username);
                    Authenticate(member);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //ModelState.AddModelError(nameof(model.Username), "Kullanıcı adı ile şifre eşleşmiyor.");
                    ModelState.AddModelError("", "Kullanıcı adı ile şifre eşleşmiyor.");
                }
            }

            return View(model);
        }

        private void Authenticate(Member member)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, member.Username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, member.Role));
            claims.Add(new Claim("confirm", member.Confirmed.ToString()));
            claims.Add(new Claim("image", member.ImageFileName));
            claims.Add(new Claim("name", member.Name));
            claims.Add(new Claim("surname", member.Surname));
            //claims.Add(new Claim("username", model.Username));
            //claims.Add(new Claim("userid", model.Username));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _accountManager.RegisterMember(model);

                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ChangeProfileInfo()
        {
            MemberUpdateViewModel model = new MemberUpdateViewModel();
            model.Name = User.FindFirst("name").Value;
            model.Surname = User.FindFirst("surname").Value;

            //string username = User.FindFirst(ClaimTypes.Name).Value;
            //Member member = _accountManager.GetMember(username);
            //model.Name = member.Name;
            //model.Surname = member.Surname;

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeProfileInfo(MemberUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                int userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                Member member = _accountManager.UpdateMember(userid, model);

                HttpContext.SignOutAsync();
                Authenticate(member);

                return RedirectToAction(nameof(Profile));
            }

            return View();
        }

        public IActionResult ChangeProfileImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeProfileImage(IFormFile picture)
        {
            int userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // wwwroot/images/1.jpg
            Stream stream = new FileStream($"wwwroot/images/{userid}.jpg", FileMode.OpenOrCreate);
            picture.CopyTo(stream);

            Member member = _accountManager.UpdateMemberPicture(userid, $"{userid}.jpg");

            //System.IO.File.Delete($"{Environment.CurrentDirectory}/wwwroot/images/{userid}.jpg");

            HttpContext.SignOutAsync();
            Authenticate(member);

            return RedirectToAction(nameof(Profile));
        }
    }
}
