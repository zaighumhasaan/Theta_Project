using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmacy_POS.Models;
using System.Collections;
using System.Reflection;

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

                                               }).ToList();
               // IList<Drug> Drug_List = _dbcontext.Drugs.ToList();
              //  var count = _dbcontext.Drugs.Count();
              //  List<string> Category_List = new List<string>(count);
              //  ArrayList Category_List = new ArrayList();

                //foreach (var name in _dbcontext.DrugCategories)
                //{
                //    foreach (var drug in Drug_List)
                //    {
                //        if (drug.DrugCategory == name.CategoryId)
                //        {
                //            Category_List.Add(name.CategoryName);
                //        }
                //    }


                //    //}
                //    //    ViewBag.CategoryList = Category_List;


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
        public IActionResult Delete_Drug(int id)
        {

            try
            {
                Drug drug = _dbcontext.Drugs.Find(id);
                if (drug != null)
                {
                    _dbcontext.Drugs.Remove(drug);
                    _dbcontext.SaveChanges();
                    
                }
                else if (drug == null)
                {
                    return View();
                }



            }
            catch (AmbiguousMatchException)
            {
            }
            return RedirectToAction(nameof(Drug_Controller.All_Drugs));
        }

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
        public IActionResult Index()
        {
            return View();
        }
    }
}
