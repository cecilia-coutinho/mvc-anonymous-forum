using AnonymousForum.Data;
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
            bool isValidUser = username == "admin" && password == "1234";

            if (isValidUser)
            {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                return RedirectToAction("Index", "Topics");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return RedirectToAction("Index", "Home");
        }
    }
}
