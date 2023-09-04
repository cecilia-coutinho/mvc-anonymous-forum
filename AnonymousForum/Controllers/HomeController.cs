using AnonymousForum.Data;
using AnonymousForum.Models;
using AnonymousForum.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnonymousForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AnonymousForumContext _context;
        public HomeController(ILogger<HomeController> logger, AnonymousForumContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var topics = _context.Topics.ToList();
            return View(topics);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}