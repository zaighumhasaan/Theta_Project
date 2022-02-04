using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
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
        public JsonResult Add_Drug_Category(string cate)
        {
            
            try
            {
                DrugCategory category = JsonConvert.DeserializeObject<DrugCategory>(cate); ;//JsonConvert.DeserializeObject<DrugCategory>(cate, new IsoDateTimeConverter());

                if (category != null)
                {

                    category.CategoryName = category.CategoryName.ToUpper();
                    if (category.DangerousLevel != null)
                    {
                        category.DangerousLevel=category.DangerousLevel.ToUpper();

                    }
                    var cat = _dbcontext.DrugCategories.Where(a => a.CategoryName == category.CategoryName).FirstOrDefault();

                    if (cat == null)
                    {
                        _dbcontext.DrugCategories.Add(category);
                        _dbcontext.SaveChanges();

                        //return Json(empDB.Add(emp), JsonRequestBehavior.AllowGet);
                       // ViewBag.SMESSAGE = "Category Added Successfully";
                        return Json(true);
                    }
                    else
                    {
                        ViewBag.EXMESSAGE = "Category  Already Exist";
                    }

                }
               

            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur While Adding Category ! Please Try Again";

            }

            return Json(false);
        }
        [HttpGet]
        public JsonResult List()
        {
            List<DrugCategory> CatList = _dbcontext.DrugCategories.Where(x => x.CategoryId != null).Select(x => new DrugCategory
            {
                CategoryId = x.CategoryId,
                CategoryName =x.CategoryName,
                DangerousLevel = x.DangerousLevel,

            }).ToList();








            return Json(CatList);
        }
        [HttpGet]
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
