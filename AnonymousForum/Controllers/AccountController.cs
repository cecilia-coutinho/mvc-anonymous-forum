using AnonymousForum.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using AnonymousForum.Models;
using Microsoft.AspNetCore.Authorization;

namespace AnonymousForum.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AnonymousForumContext _context;

        public AccountController(AnonymousForumContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Account model)
        {
            // Validate user credentials here
            string? username = "admin";
            string? password = "1234";

            if (model.Username == username && model.Password == password)
            {

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Role, "admin")
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Topics");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return RedirectToAction("Index", "Home");
        }

        //public ActionResult Login(string username, string password)
        //{
        //    bool isValidUser = username == "admin" && password == "1234";

        //    if (isValidUser)
        //    {
        //        HttpContext.Session.SetString("IsAuthenticated", "true");
        //        HttpContext.Session.SetString("Username", username);
        //        return RedirectToAction("Index", "Topics");
        //    }

        //    ViewBag.ErrorMessage = "Invalid username or password.";
        //    return RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogoutAsync()
        {
            //HttpContext.Session.Remove("IsAuthenticated");

            await HttpContext.SignOutAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
