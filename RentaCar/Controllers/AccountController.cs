using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RentaCar.Entities;
using RentaCar.Managers;
using RentaCar.Models;
using System.Security.Claims;

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

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, model.Username));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Username));
                    claims.Add(new Claim(ClaimTypes.Role, member.Role));
                    //claims.Add(new Claim("username", model.Username));
                    //claims.Add(new Claim("userid", model.Username));

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

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


    }
}
