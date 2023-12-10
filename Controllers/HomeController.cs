using Microsoft.AspNetCore.Mvc;
using MyMusicShop.Models;
using System.Diagnostics;
using MusicShop.Data;



namespace MyMusicShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MusicShopContext _context; 

        
        public HomeController(ILogger<HomeController> logger, MusicShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                           .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password );
                if (user != null)
                {
                    //Checking user privileges
                    if (user.IsAdmin)
                    {
                        HttpContext.Session.SetInt32("AdminUserId", user.UserId);
                        return RedirectToAction("Index", "Admin"); 
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("ClientUserId", user.UserId);
                        return RedirectToAction("Index", "Client"); 
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login.");
                }
            }
            return View("Index", model);
        }


    }
}
