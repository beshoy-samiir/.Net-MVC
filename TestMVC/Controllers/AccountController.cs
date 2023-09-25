using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestMVC.Models;
using TestMVC.Repository;
using TestMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel userVM)
        {
            bool found = userRepository.Find(userVM.UserName, userVM.Password);
            if (found)
            {
                TestMVC.Models.User user = userRepository.GetUser(userVM.UserName);
                List<string> roles = userRepository.GetRoles(user.Id);
                ClaimsIdentity Claims =
                    new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                Claims.AddClaim(new Claim(ClaimTypes.Name, userVM.UserName));
                Claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                Claims.AddClaim(new Claim("Name", user.Name));
                if (roles.Count > 0)
                {
                    Claims.AddClaim(new Claim(ClaimTypes.Role, roles[0]));
                }
                ClaimsPrincipal principal = new ClaimsPrincipal(Claims);

                HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("ShowAll" , "Product");
            }
            return View(userVM);
        }

        //[Authorize]
        //public IActionResult Profile()
        //{
        //    Claim nameClaim = User.Claims.FirstOrDefault(c => c.Type == "Name");
        //    string name = nameClaim.Value;
        //    return Content($"Valid Login Welcom {name}");
        //}

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegisterViewModel registerVM)
        {
            userRepository.Insert(registerVM);
            userRepository.Save();
            return RedirectToAction("ShowAll", "Product");
        }

    }
}
