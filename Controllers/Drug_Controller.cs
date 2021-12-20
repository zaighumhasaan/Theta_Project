using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Reflection;

namespace Pharmacy_POS.Controllers
{
    public class DrugController : Controller
    {
        private readonly PharmacyPOSContext _dbcontext;

        public DrugController(PharmacyPOSContext context)
        {
            _dbcontext = context;
        }
        [HttpGet]
        public IActionResult Add_Drugs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Drugs(Drug drug)
        {
            try
            {
                
                _dbcontext.Drugs.Add(drug);
                _dbcontext.SaveChanges();
                ViewBag.SMESSAGE = "Data Saved Successfully";
            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur ! Please Try Again";
                
            }

            return View(drug);  
        }

        [HttpGet]
        public IActionResult Update_Drugs(int id)
        {
            try
            {

                ViewBag.SMESSAGE = TempData["SMESSAGE"];
                ViewBag.EMESSAGE = TempData["EMESSAGE"];
                Drug drug = _dbcontext.Drugs.Find(id);
                return View(drug);


            }
            catch(AmbiguousMatchException)
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Update_Drugs(Drug drug)
        {
            try
            {

                _dbcontext.Drugs.Update(drug);
                _dbcontext.SaveChanges();
                TempData["SMESSAGE"] = "Data Updated Successfully";
  

            }
            catch (AmbiguousMatchException)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please try Agin ";
            }
            return RedirectToAction(nameof(DrugController.Update_Drugs), new { drug.DrugId});
        }

        [HttpGet]
        public IActionResult All_Drug()
        {
            try
            {
                
                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                IList<Drug> Drug_List = _dbcontext.Drugs.ToList();
                return View(Drug_List);
            }
            catch(AmbiguousMatchException)
            {

            }
            return View();
        }

        
        [HttpPost]
        public IActionResult Delete_Drug(int id)
        {

            try
            {
                Drug drug = _dbcontext.Drugs.Find(id);
                if(drug != null)
                {
                    _dbcontext.Drugs.Remove(drug);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("All_Drug");
                }
                else if(drug==null)
                {
                    return View();
                }
                

                  
            }
            catch(AmbiguousMatchException)
            {
            }
            return View();
        }
        [HttpGet]
        public IActionResult Drug_Detail(int id)
        {
            try
            {
                Drug drug = _dbcontext.Drugs.Find(id);
                if(drug!=null)
                {
                    return View(drug);
                }
                

            }catch(AmbiguousMatchException)
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
