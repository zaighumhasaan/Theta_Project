using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Reflection;
using Newtonsoft.Json;

namespace Pharmacy_POS.Controllers
{
    public class Drug_Controller : Controller
    {
        public readonly PharmacyPOSContext _dbcontext;

        public Drug_Controller(PharmacyPOSContext context)
        {
            
            _dbcontext = context;
        }

        [HttpGet]
        public IActionResult Add_Drug()
        {
            
           ViewBag.ListCategories = _dbcontext.DrugCategories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add_Drug(Drug drug)
        {
            try
            {
                
                drug.Quantity = drug.NoOfPackages * drug.NoOfUnitsInPackage;
                _dbcontext.Drugs.Add(drug);
                _dbcontext.SaveChanges();
                ViewBag.SMESSAGE = "Drug Added Successfully";
            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur While Adding Drug ! Please Try Again";

            }

            return RedirectToAction(nameof(Drug_Controller.All_Drugs));
        }
        




        [HttpGet]
        public IActionResult All_Drugs()
        {
            try
            {

                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                IList<ViewDrugs> OlistDrugs = (from drug in _dbcontext.Drugs
                                               from cat in _dbcontext.DrugCategories.Where(m => m.CategoryId == drug.DrugCategory)
                                               select new ViewDrugs {
                                                   DrugName = drug.DrugName,
                                                   ScientificName = !string.IsNullOrWhiteSpace(drug.ScientificName) ? drug.ScientificName : "",
                                                   CategoryName =cat.CategoryName,
                                                   DrugId =drug.DrugId,
                                               }).ToList();
               

                    return View(OlistDrugs);
            }
            catch (AmbiguousMatchException)
            {

            }

            return View();
        }

        [HttpGet]
        public IActionResult Update_Drug(int id)
        {
            try
            {
                ViewBag.ListCategories = _dbcontext.DrugCategories.ToList();

                ViewBag.SMESSAGE = TempData["SMESSAGE"];
                ViewBag.EMESSAGE = TempData["EMESSAGE"];
                Drug drug = _dbcontext.Drugs.Find(id);
                return View(drug);


            }
            catch (AmbiguousMatchException)
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Update_Drug(Drug drug)
        {
            try
            {

                _dbcontext.Drugs.Update(drug);
                _dbcontext.SaveChanges();
                TempData["SMESSAGE"] = "Updated Successfully";


            }
            catch (AmbiguousMatchException)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please try Agin ";
            }
            return RedirectToAction(nameof(Drug_Controller.All_Drugs));
        }
        [HttpGet]
        public JsonResult Delete_Drug(int id)
        {
            




            return Json("");
        }
        //This Action is Working But Commented For Using Ajax Action
        //[HttpGet]
        //public IActionResult Delete_Drug(int id)
        //{

        //    try
        //    {
        //        Drug drug = _dbcontext.Drugs.Find(id);
        //        if (drug != null)
        //        {
        //            _dbcontext.Drugs.Remove(drug);
        //            _dbcontext.SaveChanges();
                    
        //        }
        //        else if (drug == null)
        //        {
        //            return View();
        //        }



        //    }
        //    catch (AmbiguousMatchException)
        //    {
        //    }
        //    return RedirectToAction(nameof(Drug_Controller.All_Drugs));
        //}

        [HttpGet]
        public IActionResult Drug_Detail(int id)
        {
            try
            {
               

                

                Drug drug = _dbcontext.Drugs.Find(id);
                foreach (var name in _dbcontext.DrugCategories )
                {
                    if (drug.DrugCategory ==name.CategoryId)
                    {
                        ViewBag.Category_Name = name.CategoryName;
                    }
                }

                if (drug != null)
                {
                    return View(drug);
                }


            }
            catch (AmbiguousMatchException)
            {

            }

            return View();
        }
        [HttpPost]
        public JsonResult Get_Min_Stock()
        {
            var TenDays = DateTime.Now.AddDays(10);//This value will be used to get coming value of days for expired/going to expire  conditions
            var MinStock = _dbcontext.Drugs.Where(m => m.Quantity <= 10).Count();
            var ExpDrug = _dbcontext.Drugs.Where(m => m.ExpiryDate <= DateTime.Now).Count();
            var json = new
            {
                MinStock,
            };
            return Json(json);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
