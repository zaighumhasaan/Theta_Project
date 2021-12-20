using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Diagnostics;

namespace Pharmacy_POS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PharmacyPOSContext _dbcontext;
        public HomeController(ILogger<HomeController> logger, PharmacyPOSContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext; 
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
    }
}