using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            try
            {
               ViewBag.ListCategories = _dbcontext.DrugCategories.ToList();
               
            }
            catch (Exception ex)
            {

            }
           
            return View();
        }
        [HttpPost]
        public JsonResult Add_Drug(string drg)
        {
            try
            {
                
                Drug drug = JsonConvert.DeserializeObject<Drug>(drg, new IsoDateTimeConverter());
                if(drug!=null)
                {
                    drug.Quantity = drug.NoOfPackages * drug.NoOfUnitsInPackage;
                    _dbcontext.Drugs.Add(drug);
                    _dbcontext.SaveChanges();
                    ViewBag.SMESSAGE = "Drug Added Successfully";
                }
                return Json(true);
            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur While Adding Drug ! Please Try Again";

            }

            return Json(false);
        }
        
        [HttpGet]
        public JsonResult List()
        {
            try
            {

                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                //IList<ViewDrugs> OlistDrugs = (from drug in _dbcontext.Drugs
                //                               from cat in _dbcontext.DrugCategories.Where(m => m.CategoryId == drug.DrugCategory)
                //                               select new ViewDrugs
                //                               {
                //                                   DrugName = drug.DrugName,
                //                                   ScientificName = !string.IsNullOrWhiteSpace(drug.ScientificName) ? drug.ScientificName : "",
                //                                   CategoryName = cat.CategoryName,
                //                                   DrugId = drug.DrugId,
                //                               }).ToList();
                IList<Drug> OlistDrugs = _dbcontext.Drugs.ToList();

                return Json(OlistDrugs);
            }
            catch (AmbiguousMatchException)
            {

            }
            return Json("");
            //return Json(_dbcontext.Drugs.ToList());
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

                
                
                Drug drug = _dbcontext.Drugs.Find(id);
                ViewBag.SMESSAGE = TempData["SMESSAGE"];
                return View(drug);


            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = TempData["EMESSAGE"];
               
            }

            return View();
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
        [HttpGet]
        public JsonResult Get_Min_Stock()
        {
            try
            {
               // var TenDays = DateTime.Now.AddDays(10);//This value will be used to get coming value of days for expired/going to expire  conditions
                var MinStock = _dbcontext.Drugs.Where(m => m.Quantity <= 10).Count();
                //var ExpDrug = _dbcontext.Drugs.Where(m => m.ExpiryDate <= DateTime.Today.AddDays(10)).Count();
                if(MinStock==null)
                {
                    MinStock = 0;
                }
                //var json = new
                //{
                //    ExpDrug,
                //    MinStock,
                //};

                return Json(MinStock);
            }
            catch(Exception ex)
            {

            }
            return Json(0);
        }
        [HttpGet]
        public JsonResult Get_Expired_Drugs()
        {
            try
            {
                var ExpDrug = _dbcontext.Drugs.Where(x => x.ExpiryDate <= DateTime.Today).Count();
                if(ExpDrug==null)
                {
                    ExpDrug = 0;
                }
                return Json(ExpDrug);
            }
            catch(Exception ex)
            {

            }
         

            return Json(0);
        }
        [HttpGet]
        public JsonResult Get_Near_To_Expire_Drugs()
        {
            try
            {
                var NearExpDrug = _dbcontext.Drugs.Where(x => x.ExpiryDate <= DateTime.Today.AddDays(20)).Count();
                if (NearExpDrug == null)
                {
                    NearExpDrug = 0;
                }
                return Json(NearExpDrug);
            }
            catch (Exception ex)
            {

            }


            return Json(0);
        }
        [HttpGet]
        public JsonResult New_Stock()
        {
            try
            {
                var newstock = _dbcontext.Drugs.Where(x => x.ManufacturedDate <= DateTime.Today.AddDays(20)).Count();
                if (newstock == null)
                {
                    newstock = 0;
                }
                return Json(newstock);
            }
            catch (Exception ex)
            {

            }


            return Json(0);
        }
        [HttpGet]
        public JsonResult Delete_Drug(int Id)//Deleting Using Jquery Ajax
        {
            try
            {
                var drug = _dbcontext.Drugs.Where(x => x.DrugId == Id).FirstOrDefault();
                if (drug != null)
                {
                    _dbcontext.Drugs.Remove(drug);
                    _dbcontext.SaveChanges();
                    return Json(true);
                }

            }
            catch(Exception ex)
            {

            }
            return Json(false);

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
