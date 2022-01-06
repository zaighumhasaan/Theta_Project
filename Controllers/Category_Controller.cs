using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Reflection;

namespace Pharmacy_POS.Controllers
{
    public class Category_Controller : Controller
    {
        private readonly PharmacyPOSContext _dbcontext;
        public Category_Controller(PharmacyPOSContext context)
        {
            _dbcontext = context;
        }

        [HttpGet]
        public IActionResult Add_Drug_Category()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Drug_Category(DrugCategory category)
        {
            try
            {
                
                _dbcontext.DrugCategories.Add(category);
                _dbcontext.SaveChanges();
                ViewBag.SMESSAGE = "Category Added Successfully";
            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur While Adding Category ! Please Try Again";

            }

            return View(category);
        }
        public IActionResult AllCategories()
        {
            // show level 1 and level 2 categories
            //  IList<ItemCategory> lCategories = _dbcontext.ItemCategories.Where(o=> o.CatLevel >=1 && o.CatLevel <=2).ToList();
            // show all categories except level 1
            //  lCategories = _dbcontext.ItemCategories.Where(o => o.CatLevel !=1).ToList();
            // string ---- show all categories start with alphabet A
            var lCategories = _dbcontext.DrugCategories.ToList();
            ViewBag.SMeesage = TempData["SMessage"];
            ViewBag.EMessage = TempData["EMessage"];
            return View(lCategories);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
