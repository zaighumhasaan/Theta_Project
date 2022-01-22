using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [HttpPost]
        public JsonResult AddCustomer(Customer cust)
        {
            
           
                _dbcontext.Customers.Add(cust);
                _dbcontext.SaveChanges();
                       
            
            return new JsonResult("Inserted");
         
        }

        public IActionResult Cards()
        {
            //Drug drug = _dbcontext.Drugs.Find(15);

            //IList<ViewDrugs> OlistDrugs = (from drug in _dbcontext.Drugs.Where(d=>d.Quantity>0)
            //                               //from cat in _dbcontext.DrugCategories.Where(m => m.drug.Quantity >= 10)
            //                               select new ViewDrugs
            //                               {
            //                                   DrugId =drug.DrugId,
            //                                   Quantity = drug.Quantity,
            //                                   DrugName = drug.DrugName,

            //                                   ScientificName = !string.IsNullOrWhiteSpace(drug.ScientificName) ? drug.ScientificName : "",


            //                               }).ToList();

            ////ViewBag.no_of_min_drugs = OlistDrugs.Count();
            //String json = JsonConvert.SerializeObject(drug);


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