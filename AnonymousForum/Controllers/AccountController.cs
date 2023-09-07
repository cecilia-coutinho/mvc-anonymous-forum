using AnonymousForum.Data;
using AnonymousForum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AnonymousForum.Controllers
{
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
        public ActionResult Login(string username, string password)
        {
            var userData = DbHelper.UserData();
            bool isValidUser = !string.IsNullOrEmpty(userData.Username) && !string.IsNullOrEmpty(userData.Password);

            if (isValidUser)
            {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Topics");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("IsAuthenticated");

            return RedirectToAction("Index", "Home");
        }
    }
}
